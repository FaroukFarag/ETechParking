import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';
import { FileUpload } from 'primeng/fileupload';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { PaginatorModule } from 'primeng/paginator';
import { SelectModule } from 'primeng/select';
import { Table, TableModule } from 'primeng/table';
import { TextareaModule } from 'primeng/textarea';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { FareService } from '../../../services/locations/fares/fare.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Fare } from '../../../models/locations/fares/fare.model';
import { FieldConfig } from '../../../models/shared/field-config.model';
import { catchError, finalize, of } from 'rxjs';
import { EditFareComponent } from "./edit-fare/edit-fare.component";
import { Location } from '../../../models/locations/location.model';
import { LocationService } from '../../../services/locations/location.service';

@Component({
  selector: 'app-fares',
  imports: [
    TableModule,
    SelectModule,
    ToastModule,
    ToolbarModule,
    ConfirmDialog,
    InputTextModule,
    TextareaModule,
    FileUpload,
    DropdownModule,
    InputTextModule,
    IconFieldModule,
    InputIconModule,
    ButtonModule,
    PaginatorModule,
    EditFareComponent
  ],
  providers: [MessageService, ConfirmationService],
  templateUrl: './fares.component.html',
  styleUrl: './fares.component.css'
})
export class FaresComponent implements OnInit {
  fareService = inject(FareService);

  locationService = inject(LocationService);

  messageService = inject(MessageService);

  confirmationService = inject(ConfirmationService);

  fareDialog = false;

  fares!: Fare[];

  locations!: Location[];

  fare!: Fare;

  selectedFares!: Fare[] | null;

  selectedLocation!: Location;

  loading: boolean = false;

  @ViewChild('faresDataTable') faresDataTable!: Table;

  cols!: any[];

  exportColumns!: any[];

  fieldConfigs!: FieldConfig[];

  pageNumber: number = 0;

  pageSize: number = 1;

  rowsPerPageOptions = [1, 50, 100, 1000];

  constructor() { }

  ngOnInit(): void {
    this.loadFieldsConfig();
    this.loadData({ pageSize: this.pageSize, pageNumber: this.pageNumber });
    this.loadColumns();
    this.loadLocations();
  }

  loadFieldsConfig() {
    this.fieldConfigs = [
      { type: 'number', label: 'Amount', key: 'amount', required: true, placeholder: 'Enter amount' },
      {
        type: 'select',
        label: 'Type',
        key: 'fareType',
        required: true,
        placeholder: 'Select a location',
        options: [
          { label: 'Hourly', value: 1 },
          { label: 'Daily', value: 2 }
        ],
        onChange: this.onFareTypeChange.bind(this),
      },
      { type: 'number', label: 'Entering Grace Period', key: 'enterGracePeriod', required: true, placeholder: 'Enter entering grace period' },
      { type: 'number', label: 'Exit Grace Period', key: 'exitGracePeriod', required: true, placeholder: 'Enter exit grace period' },
      { type: 'number', label: 'Max Daily Limit', key: 'maxLimit', required: false, placeholder: 'Enter max limit', visible: false },
      {
        type: 'autocomplete',
        label: 'Location',
        key: 'locationId',
        required: true,
        placeholder: 'Select a location',
        options: this.locations,
        labelField: 'name',
        valueField: 'id',
        completeMethod: this.onCompleteMethod.bind(this),
        onSelect: this.onLocationSelect.bind(this),
      }
    ];
  }

  filterTable(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    const value = inputElement.value;

    this.faresDataTable?.filterGlobal(value, 'contains');
  }

  exportCSV() {
    this.faresDataTable.exportCSV();
  }

  loadData(event: any) {
    this.loading = true;

    const paginatedModel = {
      pageSize: event.pageSize,
      pageNumber: event.pageNumber
    };

    this.fareService.getAllPaginated('Fares/GetAllPaginated', paginatedModel).pipe(
      catchError(() => {
        this.messageService.add({
          severity: 'danger',
          summary: 'Error',
          detail: 'Failed to Load Fares!',
          life: 3000
        });

        return of([]);
      }),
      finalize(() => {
        this.loading = false;
      })
    ).subscribe((data: any[]) => {
      this.fares = data;
    });
  }

  loadLocations() {
    this.locationService.getAll('Locations/GetAll').pipe(
      catchError(() => {
        return of([]);
      })
    ).subscribe((data: any[]) => {
      this.locations = data;

      const locationField = this.fieldConfigs.find((field) => field.key === 'locationId');

      if (locationField) {
        locationField.options = this.locations;
      }
    });
  }

  loadColumns() {
    this.cols = [
      { field: 'amount', header: 'Amount' },
      { field: 'type', header: 'Type' }
    ];

    this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
  }

  onFareTypeChange(event: any) {
    const fieldConfig = this.fieldConfigs?.find(fc => fc.key === 'maxLimit');

    if (fieldConfig) {
      fieldConfig.visible = event.value === 1;
      fieldConfig.required = event.value === 1;
    }
  }

  onCompleteMethod(event: any, field: FieldConfig) {
    const query = event.query;

    field.options = this.locations.filter(location =>
      location['name'].toLowerCase().includes(query.toLowerCase())
    );
  }

  onLocationSelect(event: any, field: FieldConfig) {
    this.fare[field.key] = event.value;
  }

  openNew() {
    this.fare = new Fare();
    this.selectedLocation = undefined!;
    this.fareDialog = true;
  }

  hideDialog(show: boolean) {
    this.fareDialog = show;
  }

  deleteSelectedFares() {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected fares?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.fareService.deleteRange('Fares/DeleteRange', this.selectedFares).subscribe(data => {
          if (data == false) {
            this.messageService.add({
              severity: 'danger',
              summary: 'Error',
              detail: 'Failed to Delete Fares!',
              life: 3000
            });

            return;
          }

          this.fares = this.fares.filter((val) => !this.selectedFares?.includes(val));
          this.selectedFares = null;
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Fares Deleted',
            life: 3000
          });
        })
      }
    });
  }

  deleteFare(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this fare?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.fareService.delete(`Fares/Delete?id=${id}`).subscribe(() => {
          this.fares = this.fares.filter((val) => val.id !== id);
          this.fare = undefined!;
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Fare Deleted',
            life: 3000
          });
        });
      }
    });
  }

  editFare(fare: Fare) {
    this.fare = fare;
    this.selectedLocation = this.locations.find(l => l.id === fare?.locationId)!;
    this.fareDialog = true;
  }

  refreshFares(fares: any[]) {
    this.fares = [...fares];
  }
}

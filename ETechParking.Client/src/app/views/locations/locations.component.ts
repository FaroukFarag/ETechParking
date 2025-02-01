import { ChangeDetectorRef, Component, inject, OnInit, output, ViewChild } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { FileUpload } from 'primeng/fileupload';
import { SelectModule } from 'primeng/select';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { Table } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { LocationService } from '../../services/locations/location.service';
import { EditLocationComponent } from "./edit-location/edit-location.component";
import { FieldConfig } from '../../models/shared/field-config.model';
import { catchError, finalize, of } from 'rxjs';
import { PaginatorModule } from 'primeng/paginator';
import { Location } from '../../models/locations/location.model';

@Component({
  selector: 'app-locations',
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
    EditLocationComponent
  ],
  providers: [MessageService, ConfirmationService],
  templateUrl: './locations.component.html',
  styles: `:host ::ng-deep .p-dialog {
    width: 150px;
    margin: 0 auto 2rem auto;
    display: block;
}`
})
export class LocationsComponent implements OnInit {
  locationService = inject(LocationService);

  messageService = inject(MessageService);

  confirmationService = inject(ConfirmationService);

  locationDialog = false;

  locations!: Location[];

  location!: Location;

  selectedLocations!: Location[] | null;

  loading: boolean = false;

  @ViewChild('locationsDataTable') locationsDataTable!: Table;

  cols!: any[];

  exportColumns!: any[];

  fieldConfigs: FieldConfig[] = [
    { type: 'text', label: 'Name', key: 'name', required: true, placeholder: 'Enter name' },
    { type: 'text', label: 'Country', key: 'country', required: true, placeholder: 'Enter country' },
    { type: 'text', label: 'City', key: 'city', required: true, placeholder: 'Enter city' },
    { type: 'number', label: 'Longitude', key: 'longitude', required: true, placeholder: 'Enter longitude' },
    { type: 'number', label: 'Latitude', key: 'latitude', required: true, placeholder: 'Enter latitude' },
  ];

  pageNumber: number = 0;

  pageSize: number = 1;

  rowsPerPageOptions = [1, 50, 100, 1000];

  constructor() { }

  ngOnInit(): void {
    this.loadData({ pageSize: this.pageSize, pageNumber: this.pageNumber });
    this.loadColumns();
  }

  filterTable(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    const value = inputElement.value;

    this.locationsDataTable?.filterGlobal(value, 'contains');
  }

  exportCSV() {
    this.locationsDataTable.exportCSV();
  }

  loadData(event: any) {
    this.loading = true;

    const paginatedModel = {
      pageSize: event.pageSize,
      pageNumber: event.pageNumber
    };

    this.locationService.getAllPaginated('Locations/GetAllPaginated', paginatedModel).pipe(
      catchError(() => {
        this.messageService.add({
          severity: 'danger',
          summary: 'Error',
          detail: 'Failed to Load Locations!',
          life: 3000
        });

        return of([]);
      }),
      finalize(() => {
        this.loading = false;
      })
    ).subscribe((data: any[]) => {
      this.locations = data;
    });
  }

  loadColumns() {
    this.cols = [
      { field: 'country', header: 'Country', customExportHeader: 'Location Country' },
      { field: 'city', header: 'City' }
    ];

    this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
  }

  openNew(location: any) {
    this.location = location;
    this.locationDialog = true;
  }

  hideDialog(show: boolean) {
    this.locationDialog = show;
  }

  deleteSelectedLocations() {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected locations?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.locationService.deleteRange('Locations/DeleteRange', this.selectedLocations).subscribe(data => {
          if (data == false) {
            this.messageService.add({
              severity: 'danger',
              summary: 'Error',
              detail: 'Failed to Delete Locations!',
              life: 3000
            });

            return;
          }

          this.locations = this.locations.filter((val) => !this.selectedLocations?.includes(val));
          this.selectedLocations = null;
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Locations Deleted',
            life: 3000
          });
        })
      }
    });
  }

  deleteLocation(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this location?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.locationService.delete(`Locations/Delete?id=${id}`).subscribe(() => {
          this.locations = this.locations.filter((val) => val.id !== id);
          this.location = undefined!;
          this.messageService.add({
            severity: 'success',
            summary: 'Successful',
            detail: 'Location Deleted',
            life: 3000
          });
        });
      }
    });
  }

  editLocations(id: number) {
    this.location = this.locations.find(l => l.id === id)!;
    this.locationDialog = true;
  }

  refreshLocations(locations: any[]) {
    this.locations = [...locations];
  }
}

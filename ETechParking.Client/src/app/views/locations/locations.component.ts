import { ChangeDetectorRef, Component, inject, OnInit, ViewChild } from '@angular/core';
import { TableModule } from 'primeng/table';
import { Dialog } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { CommonModule } from '@angular/common';
import { FileUpload } from 'primeng/fileupload';
import { SelectModule } from 'primeng/select';
import { FormsModule } from '@angular/forms';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { Table } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { LocationService } from '../../services/locations/location.service';

@Component({
  selector: 'app-locations',
  imports: [
    TableModule,
    Dialog,
    SelectModule,
    ToastModule,
    ToolbarModule,
    ConfirmDialog,
    InputTextModule,
    TextareaModule,
    CommonModule,
    FileUpload,
    DropdownModule,
    InputTextModule,
    FormsModule,
    IconFieldModule,
    InputIconModule,
    ButtonModule
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

  cd = inject(ChangeDetectorRef);

  locationDialog: boolean = false;

  locations!: any[];

  location!: any;

  selectedLocations!: any[] | null;

  submitted: boolean = false;

  @ViewChild('locationsDataTable') locationsDataTable!: Table;

  cols!: any[];

  exportColumns!: any[];

  constructor() { }

  ngOnInit(): void {
    this.loadData();
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

  loadData() {
    this.locationService.getAll('Locations/GetAll').subscribe(data => {
      this.locations = data;

      this.cd.markForCheck();
    })
  }

  loadColumns() {
    this.cols = [
      { field: 'country', header: 'Country', customExportHeader: 'Location Country' },
      { field: 'city', header: 'City' }
    ];

    this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
  }

  openNew() {
    this.location = {};
    this.submitted = false;
    this.locationDialog = true;
  }

  editLocation(location: Location) {
    this.location = { ...location };
    this.locationDialog = true;
  }

  deleteSelectedLocations() {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected locations?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.locations = this.locations.filter((val) => !this.selectedLocations?.includes(val));
        this.selectedLocations = null;
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Locations Deleted',
          life: 3000
        });
      }
    });
  }

  hideDialog() {
    this.locationDialog = false;
    this.submitted = false;
  }

  deleteLocation(location: any) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + location.country + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.locations = this.locations.filter((val) => val.id !== location.id);
        this.location = {};
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Location Deleted',
          life: 3000
        });
      }
    });
  }

  findIndexById(id: string): number {
    let index = -1;
    for (let i = 0; i < this.locations.length; i++) {
      if (this.locations[i].id === id) {
        index = i;
        break;
      }
    }

    return index;
  }

  createId(): string {
    let id = '';
    var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    for (var i = 0; i < 5; i++) {
      id += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return id;
  }

  saveLocation() {
    debugger
    this.submitted = true;
    if (this.location.id) {
      this.locations[this.findIndexById(this.location.id)] = this.location;
      this.messageService.add({
        severity: 'success',
        summary: 'Successful',
        detail: 'Location Updated',
        life: 3000
      });
    } else {
      this.location.id = this.createId();
      this.locations.push(this.location);
      this.messageService.add({
        severity: 'success',
        summary: 'Successful',
        detail: 'Location Created',
        life: 3000
      });
    }

    this.locations = [...this.locations];
    this.locationDialog = false;
    this.location = {};
  }
}

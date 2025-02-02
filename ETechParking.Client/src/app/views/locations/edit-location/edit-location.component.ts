import { CommonModule } from '@angular/common';
import { Component, inject, Input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { LocationService } from '../../../services/locations/location.service';
import { MessageService } from 'primeng/api';
import { ToolbarModule } from 'primeng/toolbar';
import { ToastModule } from 'primeng/toast';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { FieldConfig } from '../../../models/shared/field-config.model';
import { Location } from '../../../models/locations/location.model';

@Component({
  selector: 'app-edit-location',
  imports: [
    CommonModule,
    FormsModule,
    SelectModule,
    ToastModule,
    ToolbarModule,
    InputTextModule,
    TextareaModule,
    DropdownModule,
    InputTextModule,
    IconFieldModule,
    InputIconModule,
    ButtonModule,
    Dialog
  ],
  templateUrl: './edit-location.component.html',
  styleUrl: './edit-location.component.css'
})
export class EditLocationComponent {
  locationService = inject(LocationService);

  messageService = inject(MessageService);

  @Input() fields!: FieldConfig[];
  
  @Input() locationDialog!: boolean;

  @Input() locations!: Location[];

  locationsChanged = output<any[]>();

  locationDialogChanged = output<boolean>();

  @Input() location!: Location;

  submitted!: boolean;

  constructor() {
  }

  ngOnInit(): void {
  }

  hideDialog() {
    this.locationDialog = false;

    this.locationDialogChanged.emit(false);

    this.submitted = false;
  }

  saveLocation(id?: number) {
    this.submitted = true;
    
    const isValid = this.fields.every(field => !field.required || !!this.location[field.key]);

    if (!isValid) {
      this.showMessage('error', 'Validation Error', 'Please fill all required fields.');
      
      return;
    }

    if (id) {
      this.updateLocation(id);
    } else {
      this.createLocation();
    }
  }

  updateLocation(id: number) {
    this.locationService.update('Locations/Update', this.location).subscribe(() => {
      const index = this.locations.findIndex(l => l.id === id);
      if (index !== -1) {
        this.locations[index] = this.location;
        this.locationsChanged.emit(this.locations);
        this.locationDialogChanged.emit(false);
        this.resetForm();
        this.showMessage('success', 'Successful', 'Location Updated');
      }
    });
  }

  private createLocation() {
    this.locationService.create('Locations/Create', this.location).subscribe(data => {
      this.location.id = data.id;
      this.locations.push(this.location);
      this.locationsChanged.emit(this.locations);
      this.locationDialogChanged.emit(false);
      this.resetForm();
      this.showMessage('success', 'Successful', 'Location Created');
    });
  }

  resetForm() {
    this.locationDialog = false;
  }

  showMessage(severity: string, summary: string, detail: string) {
    this.messageService.add({
      severity: severity,
      summary: summary,
      detail: detail,
      life: 3000
    });
  }
}


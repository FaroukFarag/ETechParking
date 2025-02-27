import { Component, inject, Input, output } from '@angular/core';
import { FareService } from '../../../../services/locations/fares/fare.service';
import { MessageService } from 'primeng/api';
import { FieldConfig } from '../../../../models/shared/field-config.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DropdownModule } from 'primeng/dropdown';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { ButtonModule } from 'primeng/button';
import { AutoComplete } from 'primeng/autocomplete';
import { Dialog } from 'primeng/dialog';
import { Fare } from '../../../../models/locations/fares/fare.model';
import { Location } from '../../../../models/locations/location.model';

@Component({
  selector: 'app-edit-fare',
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
    AutoComplete,
    Dialog
  ],
  templateUrl: './edit-fare.component.html',
  styleUrl: './edit-fare.component.css'
})
export class EditFareComponent {
  fareService = inject(FareService);

  messageService = inject(MessageService);

  @Input() fields!: FieldConfig[];

  @Input() fareDialog!: boolean;

  @Input() fares!: Fare[];

  @Input() selectedLocation!: Location;

  faresChanged = output<any[]>();

  fareDialogChanged = output<boolean>();

  @Input() fare: any;

  submitted = false;

  constructor() {
  }

  ngOnInit(): void {
  }

  hideDialog() {
    this.fareDialog = false;

    this.fareDialogChanged.emit(false);

    this.submitted = false;
  }

  saveFare(id?: number) {
    this.submitted = true;

    const isValid = this.fields.every(field => !field.required || !!this.fare[field.key]);

    if (!isValid) {
      this.showMessage('error', 'Validation Error', 'Please fill all required fields.');

      return;
    }

    if (id) {
      this.updateFare(id);
    } else {
      this.createFare();
    }
  }

  updateFare(id: number) {
    this.fareService.update('Fares/Update', this.fare).subscribe(() => {
      const index = this.fares.findIndex(f => f.id === id);
      if (index !== -1) {
        this.fares[index] = this.fare;
        this.faresChanged.emit(this.fares);
        this.fareDialogChanged.emit(false);
        this.resetForm();
        this.showMessage('success', 'Successful', 'Fare Updated');
      }
    });
  }

  private createFare() {
    this.fareService.create('Fares/Create', this.fare).subscribe(data => {
      this.fare.id = data.id;
      this.fares.push(this.fare);
      this.faresChanged.emit(this.fares);
      this.fareDialogChanged.emit(false);
      this.resetForm();
      this.showMessage('success', 'Successful', 'Fare Created');
    });
  }

  resetForm() {
    this.fareDialog = false;
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

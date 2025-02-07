import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { FieldConfig } from '../../../../models/shared/field-config.model';

@Component({
  selector: 'app-dynamic-form',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    DialogModule,
    InputTextModule,
    DropdownModule,
    ButtonModule,
    AutoCompleteModule
  ],
  templateUrl: './dynamic-dialog.component.html',
  styleUrl: './dynamic-dialog.component.css'
})
export class DynamicDialogComponent implements OnInit {
  @Input() visible: boolean = false;
  @Input() fields: FieldConfig[] = [];
  @Input() currentItem: any = {};
  @Input() dialogTitle!: string;
  @Input() pageTitle: string = 'Item Details';
  @Input() pageRoute?: string;

  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() save = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onFieldChange(field: FieldConfig, event: any) {
    if (field.onChange) {
      field.onChange(event);
    }
  }

  onAutocompleteSelect(field: FieldConfig, event: any) {
    this.currentItem[field.key] = event.value?.id || event.value;
    this.currentItem[`${field.key}Object`] = event.value;

    if (field.onSelect) {
      field.onSelect(event, field);
    }
  }

  onSave() {
    this.save.emit();
  }

  onCancel() {
    this.visibleChange.emit(false);
  }
}
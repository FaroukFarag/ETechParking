import { Component, Input, Output, EventEmitter, inject, ViewChild, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Table, TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BaseService } from '../../../services/shared/base.service';
import { FieldConfig } from '../../../models/shared/field-config.model';
import { finalize } from 'rxjs';
import { DynamicDialogComponent } from "./dynamic-dialog/dynamic-dialog.component";

@Component({
  selector: 'app-dynamic-grid',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    ToastModule,
    ToolbarModule,
    ConfirmDialogModule,
    InputTextModule,
    DropdownModule,
    ButtonModule,
    PaginatorModule,
    DialogModule,
    AutoCompleteModule,
    DynamicDialogComponent
  ],
  providers: [MessageService, ConfirmationService],
  templateUrl: './dynamic-grid.component.html',
  styleUrls: ['./dynamic-grid.component.css']
})
export class DynamicGridComponent<TModel extends { id: any }> implements OnInit {
  @ViewChild('dataTable') dataTable!: Table;

  @Input() service!: BaseService<TModel>;
  @Input() endpoint!: string;
  @Input() fields!: FieldConfig[];
  @Input() columns!: any[];
  @Input() editPageTitle = 'Edit Item';

  messageService = inject(MessageService);
  confirmationService = inject(ConfirmationService);

  items: TModel[] = [];
  selectedItems: TModel[] | null = null;
  currentItem: any = {};
  dialogVisible = false;
  loading = false;

  pageNumber = 0;
  pageSize = 10;
  rowsPerPageOptions = [10, 50, 100, 1000];

  ngOnInit(): void {
    this.loadData({ pageNumber: this.pageNumber, pageSize: this.pageSize });
  }

  loadData(event: any) {
    this.loading = true;
    const paginatedModel = {
      pageSize: event.pageSize || this.pageSize,
      pageNumber: event.pageNumber || this.pageNumber
    };

    this.service.getAllPaginated(`${this.endpoint}/GetAllPaginated`, paginatedModel)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: (data: TModel[]) => this.items = data,
        error: () => this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load data'
        })
      });
  }

  openNew() {
    this.currentItem = {};
    this.dialogVisible = true;

    this.fields.forEach(field => {
      if (field.onReset) {
        field.onReset();
      }
    });
  }

  editItem(item: TModel) {
    this.currentItem = { ...item };
    this.dialogVisible = true;

    this.fields.forEach(field => {
      if (field.type === 'autocomplete' && this.currentItem[field.key]) {
        const selectedOption = field.options?.find(opt =>
          opt.id === this.currentItem[field.key] ||
          opt[field.valueField || 'id'] === this.currentItem[field.key]
        );
        if (selectedOption) {
          this.currentItem[field.key + 'Object'] = selectedOption;
        }
      }

      if (field.onChange && this.currentItem[field.key] !== undefined) {
        field.onChange({ value: this.currentItem[field.key] });
      }
    });
  }

  deleteItem(item: TModel) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this item?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.service.delete(`${this.endpoint}/Delete?id=${item.id}`)
          .subscribe({
            next: () => {
              this.items = this.items.filter(i => i.id !== item.id);
              this.messageService.add({
                severity: 'success',
                summary: 'Successful',
                detail: 'Item Deleted'
              });
            },
            error: () => this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete item'
            })
          });
      }
    });
  }

  deleteSelectedItems() {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete selected items?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.service.deleteRange(`${this.endpoint}/DeleteRange`, this.selectedItems || [])
          .subscribe({
            next: () => {
              this.items = this.items.filter(
                item => !this.selectedItems?.some(selected => selected.id === item.id)
              );
              this.selectedItems = null;
              this.messageService.add({
                severity: 'success',
                summary: 'Successful',
                detail: 'Selected Items Deleted'
              });
            },
            error: () => this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete items'
            })
          });
      }
    });
  }

  saveItem() {
    if (!this.validateItem()) return;

    const saveMethod = this.currentItem.id ?
      this.service.update(`${this.endpoint}/Update`, this.currentItem) :
      this.service.create(`${this.endpoint}/Create`, this.currentItem);

    saveMethod.subscribe({
      next: (savedItem: TModel) => {
        if (this.currentItem.id) {
          const index = this.items.findIndex(i => i.id === savedItem.id);
          this.items[index] = savedItem;
        } else {
          this.items.push(savedItem);
        }
        this.dialogVisible = false;
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: this.currentItem.id ? 'Item Updated' : 'Item Created'
        });
      },
      error: () => this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Failed to save item'
      })
    });
  }

  validateItem(): boolean {
    const invalidFields = this.fields.filter(
      field => field.required && !this.currentItem[field.key]
    );

    if (invalidFields.length) {
      this.messageService.add({
        severity: 'error',
        summary: 'Validation Error',
        detail: `Please fill ${invalidFields.map(f => f.label).join(', ')}`
      });
      return false;
    }
    return true;
  }

  exportCSV() {
    this.dataTable.exportCSV();
  }
}

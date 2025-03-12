import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

import {
  DxButtonModule, DxDataGridModule, DxTemplateModule,
  DxPopupModule, DxSelectBoxModule, DxDateBoxModule,
  DxFormModule, DxTextAreaModule, DxTextBoxModule
} from 'devextreme-angular'; 4
import { TicketsService } from '../../services/tickets/tickets.service';
import notify from 'devextreme/ui/notify';
import { LocationService } from '../../services/locations/location.service';
import { Location } from '../../models/locations/location.model';
import { Users } from '../../models/users/users.model';
import { UsersService } from '../../services/users/users.service';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { exportDataGrid } from 'devextreme/excel_exporter';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { Workbook } from 'exceljs';
import { saveAs } from 'file-saver';
import jsPDF from 'jspdf';
import { HttpHeaders } from '@angular/common/http';
import { DxDropDownButtonModule, DxDropDownButtonComponent, DxDropDownButtonTypes } from 'devextreme-angular/ui/drop-down-button';
import { DxListModule } from 'devextreme-angular';
import { ShiftsService } from '../../services/shifts/shifts.service';
@Component({
  selector: 'app-tickets',
  standalone: true,

  imports: [CommonModule,
    DxButtonModule,
    DxDataGridModule,
    DxTemplateModule,
    DxPopupModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxDropDownButtonModule,
    DxTextBoxModule,
    DxListModule
  ], templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.scss'
})
export class TicketsComponent {
  ticketsList: any;
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];
  popupVisible = false;
  filterButtonOptions: Record<string, unknown>;
  fromDateTime: any;
  toDateTime: any;
  locationId: any;
  createUserId: any;
  closeUserId: any;
  closeButtonOptions: Record<string, unknown>;
  positionOf: string = '';
  filters: any;
  filterData = {
    fromDateTime: null,
    toDateTime: null,
    locationId: null,
    createUserId: null,
    closeUserId: null,
  };
  locationsList: any;
  usersList: any;
  selectedFormat: any;
  exportFormats: string[] = ['PDF', 'Excel', 'Word'];
  shiftId: number | null = null; 
  totalTicketsCount: number = 0;
  constructor(private ticketsService: TicketsService,
    private locationService: LocationService,
    private usersService: UsersService,
    private http: HttpClient,
    private route: ActivatedRoute,
    private shiftsService: ShiftsService) {
    this.filterButtonOptions = {
      icon: 'search',
      stylingMode: 'outlined',
      text: 'Send',
      onClick: () => {
        this.applyFilters();
      },
    };
    this.closeButtonOptions = {
      text: 'Close',
      stylingMode: 'outlined',
      type: 'normal',
      onClick: () => {
        this.popupVisible = false;
      },
    };
  }
  submitButtonOptions = {
    text: "Submit the Form",
    onClick: "applyFilters()"
  }



  showFilterPopup() {
    this.popupVisible = !this.popupVisible;
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.shiftId = params['shiftId'] ? +params['shiftId'] : null;
      if (this.shiftId) {
        this.getTicketsByShiftId(this.shiftId); 
      } else {
        this.getAllTickets();
      }
    });
    this.getAllLocations();
    this.getAllUsers();
  }

  getTicketsByShiftId(shiftId: number) {
    this.shiftsService.getShiftTickets(shiftId).subscribe((tickets: any) => {
      this.ticketsList = tickets; 
      this.totalTicketsCount = tickets.length; 
    });
  }

  getAllTickets() {
    this.ticketsService.getAll('Tickets/GetAll').subscribe((data: any) => {
      this.ticketsList = data;
    });
  }


  getAllLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: Location[]) => {
      this.locationsList = data;
    });
  }
  getAllUsers() {
    this.usersService.getAll('Users/GetAll').subscribe((data: Users[]) => {
      this.usersList = data;
    });
  }
  applyFilters() {
    const filters = {
      fromDateTime: this.filterData.fromDateTime,
      toDateTime: this.filterData.toDateTime,
      locationId: this.filterData.locationId,
      createUserId: this.filterData.createUserId,
      closeUserId: this.filterData.closeUserId,
    }

    this.ticketsService.getAllFiltered('Tickets/GetAllFiltered', filters).subscribe(
      (data: any) => {
        this.ticketsList = Array.isArray(data) ? data : [];
      },
      (error) => {
        console.error('Error applying filters:', error);
      }
    );
  }

  /*EXPORT TO EXCEL */
  onExporting(e: any) {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet('Tickets');

    exportDataGrid({
      component: e.component,
      worksheet: worksheet,
      autoFilterEnabled: true,
    }).then(() => {
      workbook.xlsx.writeBuffer().then((buffer) => {
        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Tickets.xlsx');
      });
    });
  }

  exportReport(e: any) {
    if (!this.selectedFormat) {
      notify('Please select a format to export.', 'error', 3000);
      return;
    }

    const format = this.selectedFormat;
    const filters = {
      fromDateTime: this.filterData.fromDateTime,
      toDateTime: this.filterData.toDateTime,
      locationId: this.filterData.locationId,
      createUserId: this.filterData.createUserId,
      closeUserId: this.filterData.closeUserId,
    }

    this.ticketsService.generateReport(`Reports/GetTicketsReport?format=${format}`, filters).subscribe(
      (blob: Blob | null) => {
        if (blob) { // Check if blob is not null
          const link = document.createElement('a');
          link.href = window.URL.createObjectURL(blob);
          link.download = `Tickets Report.${format}`;
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        } else {
          // Handle the case when blob is null
          notify('Failed to generate report. Please try again.', 'error', 3000);
        }
      },
      (error) => {
        if (error.error) {
          console.error('Error response body:', error.error);
        }
        notify('An error occurred while generating the report.', 'error', 3000);
      }
    );
  }



}

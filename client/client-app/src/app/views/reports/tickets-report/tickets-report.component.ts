import { Component, OnInit } from '@angular/core';
import {
  DxButtonModule, DxDataGridModule, DxTemplateModule,
  DxPopupModule, DxSelectBoxModule, DxDateBoxModule,
  DxFormModule, DxTextAreaModule, DxTextBoxModule
} from 'devextreme-angular'; 4
import { TicketsService } from '../../../services/tickets/tickets.service';
import notify from 'devextreme/ui/notify';
import { LocationService } from '../../../services/locations/location.service';
import { Location } from '../../../models/locations/location.model';
import { Users } from '../../../models/users/users.model';
import { UsersService } from '../../../services/users/users.service';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { DxDropDownButtonModule, DxDropDownButtonComponent, DxDropDownButtonTypes } from 'devextreme-angular/ui/drop-down-button';
import { DxListModule } from 'devextreme-angular';

@Component({
  selector: 'app-tickets-report',
standalone: true,

  imports: [DxButtonModule,
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
  ],
  templateUrl: './tickets-report.component.html',
  styleUrl: './tickets-report.component.scss'
})
export class TicketsReportComponent {
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
  
  constructor(private ticketsService: TicketsService,
    private locationService: LocationService,
    private usersService: UsersService,
    private http: HttpClient) {
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




  showFilterPopup() {
    this.popupVisible = !this.popupVisible;
  }

  ngOnInit() {
    this.getAllTickets();
    this.getAllLocations();
    this.getAllUsers();

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
  exportReport(e: any) {
    if (!this.selectedFormat) {
      notify('Please select a format to export.', 'error', 3000);
      return;
    }

    const filters = {
      format: this.selectedFormat,
      fromDateTime: this.filterData.fromDateTime,
      toDateTime: this.filterData.toDateTime,
      locationId: this.filterData.locationId,
      createUserId: this.filterData.createUserId,
      closeUserId: this.filterData.closeUserId,
    }

    this.ticketsService.generateReport(`Reports/DownloadTicketsReport`, filters).subscribe(
      (blob: Blob | null) => {
        if (blob) { 
          const link = document.createElement('a');
          link.href = window.URL.createObjectURL(blob);
          link.download = `Tickets Report`;
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        } else {
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

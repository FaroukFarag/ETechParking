import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxDateBoxModule, DxFormModule, DxPopupModule, DxSelectBoxModule, DxTemplateModule, DxTextAreaModule, DxTextBoxModule } from 'devextreme-angular';
import { ShiftsService } from '../../../services/shifts/shifts.service';
import { LocationService } from '../../../services/locations/location.service';
import { Location } from '../../../models/locations/location.model';
import { Users } from '../../../models/users/users.model';
import { UsersService } from '../../../services/users/users.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import notify from 'devextreme/ui/notify';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shifts-report',
  standalone: true,
  imports: [DxButtonModule,
    DxDataGridModule,
    DxTemplateModule,
    DxPopupModule,
    DxSelectBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxFormModule,
    DxTextBoxModule,
    CommonModule],
  templateUrl: './shifts-report.component.html',
  styleUrl: './shifts-report.component.scss'
})
export class ShiftsReportComponent {
  shiftsList: any;
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];
  popupVisible = false;
  locationsList: any;
  usersList: any;
  fromDateTime: any;
  toDateTime: any;
  locationId: any;
  createUserId: any;
  closeUserId: any;
  shiftssList: any;
  filterData = {
    fromDateTime: null,
    toDateTime: null,
    locationId: null,
    cashierUserId: null,
    accountantUserId: null,
  };
  selectedFormat: any;
  exportFormats: string[] = ['PDF', 'Excel', 'Word'];
  constructor(private shiftsService: ShiftsService, private locationService: LocationService, private usersService: UsersService, private http: HttpClient) {

  }



  ngOnInit() {
    this.getAllShifts();
    this.getAllLocations();
    this.getAllUsers();
  }


  showFilterPopup() {
    this.popupVisible = !this.popupVisible;
  }
  getAllShifts() {
    this.locationService.getAll('Shifts/GetAll').subscribe((data: Location[]) => {
      this.shiftsList = data;
    });
  }
  getAllLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: Location[]) => {
      this.locationsList = data;
    });
  }
  getAllUsers() {
    this.usersService.getAll('Users/GetAll').subscribe((data: Users[]) => {
      this.usersList = data
    });
  }


  applyFilters() {
    const filters = {
      fromDateTime: this.filterData.fromDateTime,
      toDateTime: this.filterData.toDateTime,
      locationId: this.filterData.locationId,
      cashierUserId: this.filterData.cashierUserId,
      accountantUserId: this.filterData.accountantUserId,
    }
    this.shiftsService.getAllFiltered('Shifts/GetAllFiltered', filters).subscribe(
      (data: any) => {
        this.shiftsList = Array.isArray(data) ? data : [];
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

    const format = this.selectedFormat;
    const filters = {
      fromDateTime: this.filterData.fromDateTime,
      toDateTime: this.filterData.toDateTime,
      locationId: this.filterData.locationId,
      cashierUserId: this.filterData.cashierUserId,
      accountantUserId: this.filterData.accountantUserId,
    }

    this.shiftsService.generateReport(`Reports/DownloadShiftsReport?format=${format}`, filters).subscribe(
      (blob: Blob | null) => {
        if (blob) { // Check if blob is not null
          const link = document.createElement('a');
          link.href = window.URL.createObjectURL(blob);
          link.download = `Shifts Report.${format}`;
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

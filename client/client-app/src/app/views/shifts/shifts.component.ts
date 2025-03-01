import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxDateBoxModule, DxFormModule, DxPopupModule, DxSelectBoxModule, DxTemplateModule, DxTextAreaModule, DxTextBoxModule } from 'devextreme-angular';
import { ShiftsService } from '../../services/shifts/shifts.service';
import { LocationService } from '../../services/locations/location.service';
import { Location } from '../../models/locations/location.model';
import { Users } from '../../models/users/users.model';
import { UsersService } from '../../services/users/users.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import notify from 'devextreme/ui/notify';
import { exportDataGrid } from 'devextreme/excel_exporter';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { Workbook } from 'exceljs';
import { saveAs } from 'file-saver';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shifts',
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
  templateUrl: './shifts.component.html',
  styleUrl: './shifts.component.scss'
})
export class ShiftsComponent {
  shiftsList: any[] = [];
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
    createUserId: null,
    closeUserId: null,

  };

  selectedFormat: any;
  exportFormats: string[] = ['PDF', 'Excel', 'Word'];

  viewPopupVisible = false;

  viewFormData: any = {
    id: 0,

    totalCash: 0,

    totalCredit: 0,

    endDateTime: new Date().toISOString(),

  };
  saveShiftButtonOptions: any;
  constructor(private shiftsService: ShiftsService, private locationService: LocationService, private usersService: UsersService, private http: HttpClient) {
    this.saveShiftButtonOptions = {
      icon: 'save',
      stylingMode: 'contained',
      type:'normal',
      text: 'Save',
      onClick: () => {
        this.saveShift();
      },
    };
  }



  ngOnInit() {
    this.getAllShifts();
    this.getAllLocations();
    this.getAllUsers();
  }





  openViewPopup(event: any) {
    this.viewFormData = {

      id: event.data.id,

      totalCash: 0,

      totalCredit: 0,

      endDateTime: new Date().toISOString(),

    };

    this.viewPopupVisible = true;

  }


  saveShift() {
    console.log("fidjdgj")
    debugger
    const closeShiftData = {

      id: this.viewFormData.id,

      totalCash: this.viewFormData.totalCash,

      totalCredit: this.viewFormData.totalCredit,

      endDateTime: this.viewFormData.endDateTime,

    };


    this.shiftsService.closeShift('Shifts/CloseShift', closeShiftData).subscribe(

      () => {

        this.getAllShifts(); 

        this.viewPopupVisible = false; 
      },

      (error) => {

        console.error('Error closing shift:', error);

      }

    );

  }



  showFilterPopup() {
    this.popupVisible = !this.popupVisible;
  }
  getAllShifts() {
    this.locationService.getAll('Shifts/GetAll').subscribe((data: Location[]) => {
      this.shiftsList = Array.isArray(data) ? data : [];    });
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

  onRowInserting(e: any) {
    this.shiftsService.create('Shifts/Create', e.data).subscribe(() => {
      this.getAllLocations();
    });
  }




  onRowUpdating(e: any) {
    const updatedData = { ...e.oldData, ...e.newData };
    this.shiftsService.update('Shifts/Update', updatedData).subscribe(
      () => {
        this.getAllLocations();
      },
      (error) => {
        alert("Failed to update location: " + (error.error.message || "Unknown error"));
      }
    );
  }

  onRowRemoving(e: any) {
    const shiftId = e.data.id;
    this.shiftsService.delete(`Shifts/Delete?id=${shiftId}`).subscribe(() => {
      this.getAllLocations();
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
    this.shiftsService.generateReport(`Reports/GetShiftsReport?format=${format}`).subscribe(
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


  /*EXPORT TO EXCEL */
  onExporting(e: any) {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet('Shifts');

    exportDataGrid({
      component: e.component,
      worksheet: worksheet,
      autoFilterEnabled: true,
    }).then(() => {
      workbook.xlsx.writeBuffer().then((buffer) => {
        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Shifts.xlsx');
      });
    });
  }

  closeShift(shift: any) {
    const closeShiftData = {
      endDateTime: new Date().toISOString(), 
      totalCash: 0,
      totalCredit: 0 
    };

    this.shiftsService.closeShift('Shifts/CloseShift', closeShiftData).subscribe(
      () => {
        this.getAllShifts();
      },
      (error) => {
        console.error('Error closing shift:', error);
      }
    );
  }
}

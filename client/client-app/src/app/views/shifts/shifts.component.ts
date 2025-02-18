import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxDateBoxModule, DxFormModule, DxPopupModule, DxSelectBoxModule, DxTemplateModule, DxTextAreaModule, DxTextBoxModule } from 'devextreme-angular';
import { ShiftsService } from '../../services/shifts/shifts.service';
import { LocationService } from '../../services/locations/location.service';
import { Location } from '../../models/locations/location.model';
import { Users } from '../../models/users/users.model';
import { UsersService } from '../../services/users/users.service';
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
    DxTextBoxModule, ],
  templateUrl: './shifts.component.html',
  styleUrl: './shifts.component.scss'
})
export class ShiftsComponent {
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
    createUserId: null,
    closeUserId: null,
  };
  constructor(private shiftsService: ShiftsService, private locationService: LocationService, private usersService: UsersService) {

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
      this.usersList = data;
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
    };

    this.shiftsService.getAllFiltered('Shifts/GetAllFiltered',filters).subscribe(
      (data: any) => {
        this.shiftssList = data;
        this.popupVisible = false;
      },
      (error) => {
        console.error('Error applying filters:', error);
      }
    );
  }
}

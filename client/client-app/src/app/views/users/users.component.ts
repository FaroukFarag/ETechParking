import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';
import { UsersService } from '../../services/users/users.service';
import { LocationService } from '../../services/locations/location.service';
import { Location } from '../../models/locations/location.model';

@Component({
  selector: 'app-users',
  standalone: true,

  imports: [DxButtonModule,
    DxDataGridModule,
  ], templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {
  usersList: any;
  rolesList: any;
  locations: Location[] = [];
  roleId: any;
  locationId: any;
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];

  constructor(
    private usersService: UsersService,
    private locationService: LocationService) {

  }

  ngOnInit() {
    this.getAllUsers();
    this.getRoles();
    this.getAllLocations();
  }

  getAllUsers() {
    this.usersService.getAll('Users/GetAll').subscribe((data: any) => {
      this.usersList = data;
    })
  }

  getRoles() {
    this.usersService.getAll('Roles/GetAll').subscribe((data: any) => {
      this.rolesList = data;
    })
  }

  getAllLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: Location[]) => {
      this.locations = data;
    });

  }

  onRowInserting(e: any) {
    this.usersService.create('Users/Create', e.data).subscribe(() => {
      this.getAllUsers();
    });
  }

  onRowUpdating(e: any) {
    const updatedData = { ...e.oldData, ...e.newData };
    this.usersService.update('Users/Update', updatedData).subscribe(
      () => {
        this.getAllUsers();
      },
      (error) => {
        alert("Failed to update user: " + (error.error.message || "Unknown error"));
      }
    );
  }

  onRowRemoving(e: any) {
    const userId = e.data.id;
    this.usersService.delete(`Users/Delete?id=${userId}`).subscribe(() => {
      this.getAllUsers(); // Refresh the list after deleting
    });
  }
}

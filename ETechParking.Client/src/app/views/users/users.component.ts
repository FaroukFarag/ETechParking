import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';
import { UsersService } from '../../services/users/users.service';

@Component({
  selector: 'app-users',
  imports: [DxButtonModule,
    DxDataGridModule,
  ],  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent {
  usersList: any;
  rolesList: any;
  allowedPageSizes: boolean = true;
  constructor(private usersService: UsersService) {

  }



  ngOnInit() {
    this.getAllUsers();
    this.getRoles();

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

import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';
import { } from 'devextreme-angular';
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
  allowedPageSizes: boolean = true;
  constructor(private usersService: UsersService) {

  }



  ngOnInit() {
    this.getAllLocations();
  }


  getAllLocations() {
    this.usersService.getAll('Users/GetAll').subscribe((data: any) => {
      this.usersList = data;
      console.log("locations", this.usersList);
    })
  }
  onRowInserting(e: any) {
    this.usersService.create('Users/Create', e.data).subscribe(() => {
      this.getAllLocations(); // Refresh the list after adding
    });
  }


  onRowUpdating(e: any) {
    this.usersService.update('Users/Update', e.data).subscribe(() => {
      this.getAllLocations(); // Refresh the list after adding
    });
  }

  onRowRemoving(e: any) {
    const userId = e.data.id;
    console.log("ID", userId);
    this.usersService.delete(`Users/Delete?id=${userId}`).subscribe(() => {
      this.getAllLocations(); // Refresh the list after deleting
    });
  }
}

import { Component, OnInit } from '@angular/core';
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

@Component({
  selector: 'app-tickets',
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
  positionOf: string='';
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

  constructor(private ticketsService: TicketsService, private locationService: LocationService, private usersService: UsersService) {
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


    //applyFilters() {
    //    throw new Error('Method not implemented.');
    //}




 
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





  onRowInserting(e: any) {
   
  }


  onRowUpdating(e: any) {

  }

  onRowRemoving(e: any) {
    
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
    };

    this.ticketsService.getAllFiltered('Tickets/GetAllFiltered', filters).subscribe(
      (data: any) => {
        this.ticketsList = data;
        this.popupVisible = false;
      },
      (error) => {
        console.error('Error applying filters:', error);
      }
    );
  }

}

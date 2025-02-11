import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';
import { TicketsService } from '../../services/tickets/tickets.service';


@Component({
  selector: 'app-tickets',
  imports: [DxButtonModule,
    DxDataGridModule,
  ],  templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.css'
})
export class TicketsComponent {
  ticketsList: any;
  allowedPageSizes: boolean = true;
  constructor(private ticketsService: TicketsService) {

  }

  ngOnInit() {
    this.getAllTickets();
  }


  getAllTickets() {
    this.ticketsService.getAll('Tickets/GetAll').subscribe((data: any) => {
      this.ticketsList = data;
    })
  }
  onRowInserting(e: any) {
   
  }


  onRowUpdating(e: any) {

  }

  onRowRemoving(e: any) {
    
  }
}

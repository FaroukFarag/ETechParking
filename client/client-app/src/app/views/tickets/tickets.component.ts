import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxTemplateModule, DxPopupModule } from 'devextreme-angular';
import { TicketsService } from '../../services/tickets/tickets.service';
import notify from 'devextreme/ui/notify';


@Component({
  selector: 'app-tickets',
  standalone: true,

  imports: [DxButtonModule,
    DxDataGridModule,
    DxTemplateModule,
    DxPopupModule
  ], templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.scss'
})
export class TicketsComponent {
  ticketsList: any;
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];
  popupVisible = false;
  filterButtonOptions: Record<string, unknown>;

  closeButtonOptions: Record<string, unknown>;
  positionOf: string='';

  constructor(private ticketsService: TicketsService) {
    this.filterButtonOptions = {
      icon: 'search',
      stylingMode: 'outlined',
      text: 'Send',
      onClick: () => {
        notify({
          onmessage,
          position: {
            my: 'center top',
            at: 'center top',
          },
        }, 'success', 3000);
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
    this.popupVisible = true;
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

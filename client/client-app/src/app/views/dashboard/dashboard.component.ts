import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DxSelectBoxTypes } from 'devextreme-angular/ui/select-box';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { DxSelectBoxModule } from 'devextreme-angular';
import { LocationService } from '../../services/locations/location.service';
import { ShiftsService } from '../../services/shifts/shifts.service';
import { TicketsService } from '../../services/tickets/tickets.service';
import { DxPieChartModule } from 'devextreme-angular';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DxSelectBoxModule,
    DxPieChartModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  locationsList: any;
  searchModeOption = 'contains';
  searchExprOption = 'Name';
  searchTimeoutOption = 200;
  minSearchLengthOption = 0;
  totalShifts = 0;
  totalConfirmedShifts = 0;
  totalTickets = 0;
  totalPaidTickets = 0;
  showDataBeforeSearchOption = false;

  ticketsTransactionType:any=[] =[{
    type: 'Cash',
    value: 70,
  }, {
    type: 'Credit',
    value: 30,
    },];

    ticketsClientType:any=[] =[{
    type: 'Guest',
    value: 40,
  }, {
    type: 'Visitor',
    value: 60,
  }, ];


  constructor(
    private locationService: LocationService,
    private shiftService: ShiftsService,
    private ticketsService: TicketsService) { }

  ngOnInit() {
    this.getLocationsList();

    this.getTotalShifts();

    this.getTotalConfirmedShifts();

    this.getTotalTickets();

    this.getTotalPaidTickets();
  }

  customizeLabel(arg: any) {
    return `${arg.valueText} (${arg.percentText})`;
  }
  getLocationsList() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: any) => {
      this.locationsList = data;
    })
  }

  getTotalShifts() {
    this.shiftService.getTotalShifts().subscribe((data: any) => {
      this.totalShifts = data;
    })
  }

  getTotalConfirmedShifts() {
    this.shiftService.getTotalShifts(3).subscribe((data: any) => {
      this.totalConfirmedShifts = data;
    })
  }

  getTotalTickets() {
    this.shiftService.getTotalShifts().subscribe((data: any) => {
      this.totalTickets = data;
    })
  }

  getTotalPaidTickets() {
    this.ticketsService.getTotalTickets(true).subscribe((data: any) => {
      this.totalPaidTickets = data;
    })
  }
}

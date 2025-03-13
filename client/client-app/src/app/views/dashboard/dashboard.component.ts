import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DxSelectBoxTypes } from 'devextreme-angular/ui/select-box';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { DxSelectBoxModule } from 'devextreme-angular';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { LocationService } from '../../services/locations/location.service';
import { ShiftsService } from '../../services/shifts/shifts.service';
import { TicketsService } from '../../services/tickets/tickets.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DxSelectBoxModule,
    CanvasJSAngularChartsModule],
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

  chartOptions = {
    animationEnabled: false,
    theme: "light",
    exportEnabled: false,
    title: {
      text: ""
    },
    subtitles: [{
      text: ""
    }],
    data: [{
      type: "doughnut", //change type to column, line, area, doughnut, etc
      indexLabel: "{name}: {y}%",
      dataPoints: [
        { name: "Total Shifts", y: 9.1 },
        { name: "Total Closed Shifts", y: 3.7 },
        { name: "Total Tickets", y: 36.4 },
        { name: "Total Paid Tickets", y: 30.7 },
        { name: "Total Rates", y: 20.1 }
      ]
    }]
  }


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

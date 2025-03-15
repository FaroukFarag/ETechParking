import { Component, NgModule } from '@angular/core';
import { DxSelectBoxModule, DxDateBoxModule } from 'devextreme-angular';
import { LocationService } from '../../services/locations/location.service';
import { ShiftsService } from '../../services/shifts/shifts.service';
import { TicketsService } from '../../services/tickets/tickets.service';
import { DxPieChartModule } from 'devextreme-angular';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DxSelectBoxModule,
    DxDateBoxModule,
    DxPieChartModule,
    FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  locationsList: any;
  searchModeOption = 'contains';
  searchExprOption = 'Name';
  searchTimeoutOption = 200;
  selectedLocationId: any = null;
  selectedDate: any = null;
  minSearchLengthOption = 0;
  totalShifts = 0;
  totalConfirmedShifts = 0;
  totalTickets = 0;
  totalPaidTickets = 0;
  totalLocations = 0;
  showDataBeforeSearchOption = false;
  ticketsTransactionType: any = [];
  ticketsClientType: any = [];

  constructor(
    private locationService: LocationService,
    private shiftService: ShiftsService,
    private ticketsService: TicketsService) { }

  ngOnInit() {
    this.getLocationsList();

    this.getTotalLocations();

    this.getDashboardTotals();
  }

  customizeLabel(arg: any) {
    return `${arg.valueText} (${arg.percentText})`;
  }
  getLocationsList() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: any) => {
      this.locationsList = data;
    })
  }

  onLocationChange(value: any) {
    this.selectedLocationId = value;

    this.getDashboardTotals();
  }

  onDateChange(value: any) {
    this.selectedDate = value;

    this.getDashboardTotals();
  }

  getTotalShifts() {
    const req = {
      locationId: this.selectedLocationId,
      day: this.selectedDate
    }
    this.shiftService.getTotalShifts(req).subscribe((data: any) => {
      this.totalShifts = data;
    })
  }

  getTotalConfirmedShifts() {
    const req = {
      locationId: this.selectedLocationId,
      day: this.selectedDate,
      status: 3
    }
    this.shiftService.getTotalShifts(req).subscribe((data: any) => {
      this.totalConfirmedShifts = data;
    })
  }

  getTotalTickets() {
    const req = {
      locationId: this.selectedLocationId,
      day: this.selectedDate
    }
    this.ticketsService.getTotalTickets(req).subscribe((data: any) => {
      this.totalTickets = data;
    })
  }

  getTotalPaidTickets() {
    const req = {
      locationId: this.selectedLocationId,
      day: this.selectedDate,
      isPaid: true
    }
    this.ticketsService.getTotalTickets(req).subscribe((data: any) => {
      this.totalPaidTickets = data;
    })
  }

  getTotalLocations() {
    this.locationService.getTotalLocations().subscribe((data: any) => {
      this.totalLocations = data;
    })
  }

  getTicketsTransactionType() {
    const req = {
      locationId: this.selectedLocationId,
      day: this.selectedDate
    }

    this.ticketsService.getTicketsTransactionType(req).subscribe({
      next: (data: any) => {
        this.ticketsTransactionType = data;
      },
      error: (error: any) => {
        console.error('Error fetching ticket transaction types:', error);
        this.ticketsTransactionType = [];
      },
      complete: () => {
        console.log('Transaction type data fetched successfully.');
      }
    });
  }

  getTicketsClientType() {
    const req = {
      locationId: this.selectedLocationId,
      day: this.selectedDate
    }

    this.ticketsService.getTicketsClientType(req).subscribe({
      next: (data: any) => {
        this.ticketsClientType = data;
      },
      error: (error: any) => {
        console.error('Error fetching ticket client types:', error);
        this.ticketsClientType = [];
      },
      complete: () => {
        console.log('Client type data fetched successfully.');
      }
    });
  }

  getDashboardTotals() {
    this.getTotalShifts();

    this.getTotalConfirmedShifts();

    this.getTotalTickets();

    this.getTotalPaidTickets();

    this.getTicketsTransactionType();

    this.getTicketsClientType();
  }
}

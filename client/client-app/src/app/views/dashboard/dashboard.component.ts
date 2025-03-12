import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DxSelectBoxTypes } from 'devextreme-angular/ui/select-box';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { DxSelectBoxModule } from 'devextreme-angular';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { LocationService } from '../../services/locations/location.service';

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


  constructor(private locationService: LocationService) { }

  ngOnInit() {
    this.getLocationsList();
  }


  getLocationsList() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: any) => {
      this.locationsList = data;
      console.log(this.locationsList);
    })
  }
}

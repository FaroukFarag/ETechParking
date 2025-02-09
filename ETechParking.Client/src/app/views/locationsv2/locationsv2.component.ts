import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';
import { } from 'devextreme-angular';
import { LocationService } from '../../services/locations/location.service';

@Component({
  selector: 'app-locationsv2',
  imports: [DxButtonModule,
    DxDataGridModule,
  ],
  templateUrl: './locationsv2.component.html',
  styleUrl: './locationsv2.component.css'
})
export class Locationsv2Component {
  locationsList: any;
  allowedPageSizes: boolean = true;
  constructor(private locationService: LocationService) {

  }



  ngOnInit() {
    this.getAllLocations();
  }


  getAllLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: any) => {
      this.locationsList = data;
      console.log("locations", this.locationsList);
    })
  }
  onRowInserting(e: any) {
    this.locationService.create('Locations/Create',e.data).subscribe(() => {
      this.getAllLocations(); // Refresh the list after adding
    });
  }


  onRowUpdating(e: any) {
    this.locationService.update('Locations/Update', e.data).subscribe(() => {
      this.getAllLocations(); // Refresh the list after adding
    });
  }

  onRowRemoving(e: any) {
    const LocationId = e.data.id;
    console.log("ID", LocationId);
    this.locationService.delete(`Locations/Delete?id=${LocationId}`).subscribe(() => {
      this.getAllLocations(); // Refresh the list after deleting
    });
  }
}

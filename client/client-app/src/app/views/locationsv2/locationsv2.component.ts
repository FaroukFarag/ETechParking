import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxPopupModule } from 'devextreme-angular';
import { LocationService } from '../../services/locations/location.service';

@Component({
  selector: 'app-locationsv2',
  standalone: true,

  imports: [DxButtonModule,
    DxDataGridModule,
    DxPopupModule 
  ],
  templateUrl: './locationsv2.component.html',
  styleUrl: './locationsv2.component.scss'
})
export class Locationsv2Component {
  locationsList: any;
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];
  constructor(private locationService: LocationService) {

  }



  ngOnInit() {
    this.getAllLocations();
  }


  getAllLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: any) => {
      this.locationsList = data;
      console.log(this.locationsList);
    })
  }
  onRowInserting(e: any) {
    this.locationService.create('Locations/Create',e.data).subscribe(() => {
      this.getAllLocations(); 
    });
  }


 

  onRowUpdating(e: any) {
    const updatedData = { ...e.oldData, ...e.newData };
    this.locationService.update('Locations/Update', updatedData).subscribe(
      () => {
        this.getAllLocations(); 
      },
      (error) => {
        alert("Failed to update location: " + (error.error.message || "Unknown error"));
      }
    );
  }

  onRowRemoving(e: any) {
    const LocationId = e.data.id;
    this.locationService.delete(`Locations/Delete?id=${LocationId}`).subscribe(() => {
      this.getAllLocations(); 
    });
  }
}

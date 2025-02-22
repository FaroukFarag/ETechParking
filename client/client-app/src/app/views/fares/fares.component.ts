import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';
import { } from 'devextreme-angular';
import { FareService } from '../../services/locations/fares/fare.service';
import { LocationService } from '../../services/locations/location.service';
import { Location } from '../../models/locations/location.model';
import { exportDataGrid } from 'devextreme/excel_exporter';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { Workbook } from 'exceljs';
import { saveAs } from 'file-saver';
@Component({
  standalone: true,
  selector: 'app-fares',
  imports: [DxButtonModule,
    DxDataGridModule],
  templateUrl: './fares.component.html',
  styleUrl: './fares.component.scss'
})
export class FaresComponent {
  faresList: any;
  locations: Location[] = [];
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];
  constructor(private faresService: FareService, private locationService: LocationService) { }

  ngOnInit() {
    this.getAllFares();
    this.getAllLocations();
  }

  getAllFares() {
    this.faresService.getAll('Fares/GetAll').subscribe((data: any) => {
      this.faresList = data;
    })
  }
  getAllLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: Location[]) => {
      this.locations = data; 
    });

  }
  onRowInserting(e: any) {
    this.faresService.create('Fares/Create', e.data).subscribe(() => {
      this.getAllFares(); 
    });
  }

  onRowUpdating(e: any) {
    const updatedData = { ...e.oldData, ...e.newData };
    this.faresService.update('Fares/Update', updatedData).subscribe(
      () => {
        this.getAllFares();
      },
      (error) => {
        alert("Failed to update Fare: " + (error.error.message || "Unknown error"));
      }
    );
  }

  onRowRemoving(e: any) {
    const fareId = e.data.id;
    this.faresService.delete(`Fares/Delete?id=${fareId}`).subscribe(() => {
      this.getAllFares(); // Refresh the list after deleting
    });
  }

  /*EXPORT TO EXCEL */
  onExporting(e: any) {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet('Fares');

    exportDataGrid({
      component: e.component,
      worksheet: worksheet,
      autoFilterEnabled: true,
    }).then(() => {
      workbook.xlsx.writeBuffer().then((buffer) => {
        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Fares.xlsx');
      });
    });
  }
}

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
import { CommonModule } from '@angular/common';
@Component({
  standalone: true,
  selector: 'app-fares',
  imports: [
    DxButtonModule,
    DxDataGridModule,
    CommonModule],
  templateUrl: './fares.component.html',
  styleUrl: './fares.component.scss'
})
export class FaresComponent {
  faresList: any;
  locations: Location[] = [];
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];
  locationId: any;
  locationEditorOptions: any;
  fareTypes = [{ id: 1, name: 'Hourly' }, { id: 2, name: 'Daily' }]
  fareTypeEditorOptions: any;
  showMaxLimit = false;
  enterGracePeriodEditorOptions: any;
  exitGracePeriodEditorOptions: any;
  fareType: any;
  constructor(private faresService: FareService, private locationService: LocationService) {
    this.enterGracePeriodEditorOptions = {
      editorType: 'dxNumberBox',
      label: this.getGracePeriodLabel('enter')
    };
    this.exitGracePeriodEditorOptions = {
      editorType: 'dxNumberBox',

      label: this.getGracePeriodLabel('exit')
    };

  }


  ngOnInit() {
    this.getAllFares();
    this.getAllLocations();
    this.loadFareTypeEditorOptions();
  }





  getAllFares() {
    this.faresService.getAll('Fares/GetAll').subscribe((data: any) => {
      this.faresList = data;
    })
  }

  getAllLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe((data: Location[]) => {
      this.locations = data;
      this.loadLocationEditorOptions();
    });

  }

  loadLocationEditorOptions() {
    this.locationEditorOptions = {
      dataSource: this.locations,
      valueExpr: 'id',
      displayExpr: 'name',
      value: this.locationId
    }
  }

  loadFareTypeEditorOptions() {
    this.fareTypeEditorOptions = {
      dataSource: this.fareTypes,
      valueExpr: 'id',
      displayExpr: 'name',
      value: this.fareType,
      onValueChanged: this.onFareTypeChange.bind(this)
    }
  }

  onInitNewRow(e: any) {
    this.showMaxLimit = false;
  }

  onRowInserting(e: any) {
    e.data.fareType = this.fareType;

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
      this.getAllFares(); 
    });
  }

  onEditingStart(e: any) {
    this.fareType = e.data.fareType;
    this.showMaxLimit = this.fareType === 1;
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

  onFareTypeChange(event: any) {
    this.fareType = event.value;
    this.fareTypeEditorOptions.value = event.value;
    this.showMaxLimit = this.fareType === 1;
  }

  getGracePeriodLabel(type: string) {
    if (this.fareType === 1) {
      return { text: `${type.charAt(0).toUpperCase() + type.slice(1)} grace period (Hours)` };
    } else if (this.fareType === 2) {
      return { text: `${type.charAt(0).toUpperCase() + type.slice(1)} grace period (Minutes)` };
    } else {
      return { text: `${type.charAt(0).toUpperCase() + type.slice(1)} grace period` };
    }
  }

}

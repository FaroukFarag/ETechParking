import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule } from 'devextreme-angular';
import { } from 'devextreme-angular';
import { FareService } from '../../services/locations/fares/fare.service';

@Component({
  selector: 'app-fares',
  imports: [DxButtonModule,
    DxDataGridModule],
  templateUrl: './fares.component.html',
  styleUrl: './fares.component.css'
})
export class FaresComponent {
  faresList: any;
  allowedPageSizes: boolean = true;
  constructor(private faresService: FareService) { }

  ngOnInit() {
    this.getAllFares();
  }


  getAllFares() {
    this.faresService.getAll('Fares/GetAll').subscribe((data: any) => {
      this.faresList = data;
    })
  }
  onRowInserting(e: any) {
    this.faresService.create('Fares/Create', e.data).subscribe(() => {
      this.getAllFares(); // Refresh the list after adding
    });
  }


  onRowUpdating(e: any) {
    this.faresService.update('Fares/Update', e.data).subscribe(() => {
      this.getAllFares(); // Refresh the list after adding
    });
  }

  onRowRemoving(e: any) {
    const fareId = e.data.id;
    console.log("ID", fareId);
    this.faresService.delete(`Fares/Delete?id=${fareId}`).subscribe(() => {
      this.getAllFares(); // Refresh the list after deleting
    });
  }
}

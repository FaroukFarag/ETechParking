import { Component, OnInit } from '@angular/core';
import { DxButtonModule, DxDataGridModule, DxPopupModule } from 'devextreme-angular';
import { ShiftsService } from '../../services/shifts/shifts.service';

@Component({
  selector: 'app-shifts',
  standalone: true,
  imports: [],
  templateUrl: './shifts.component.html',
  styleUrl: './shifts.component.scss'
})
export class ShiftsComponent {
  shiftsList: any;
  allowedPageSizes: (number | "auto")[] = [10, 20, 50];
  constructor(private shiftsService: ShiftsService) {

  }



  ngOnInit() {
    this.getAllLocations();
  }


  getAllLocations() {
    this.shiftsService.getAll('Shifts/GetAll').subscribe((data: any) => {
      this.shiftsList = data;
      console.log(this.shiftsList);
    })
  }
  onRowInserting(e: any) {
    this.shiftsService.create('Shifts/Create', e.data).subscribe(() => {
      this.getAllLocations();
    });
  }




  onRowUpdating(e: any) {
    const updatedData = { ...e.oldData, ...e.newData };
    this.shiftsService.update('Shifts/Update', updatedData).subscribe(
      () => {
        this.getAllLocations();
      },
      (error) => {
        alert("Failed to update location: " + (error.error.message || "Unknown error"));
      }
    );
  }

  onRowRemoving(e: any) {
    const shiftId = e.data.id;
    this.shiftsService.delete(`Shifts/Delete?id=${shiftId}`).subscribe(() => {
      this.getAllLocations();
    });
  }
}

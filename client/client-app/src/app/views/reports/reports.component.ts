import { Component } from '@angular/core';
import { DxTabsModule, DxSelectBoxModule, DxMultiViewModule } from 'devextreme-angular';
import { TicketsComponent } from '../tickets/tickets.component';
import { ShiftsComponent } from '../shifts/shifts.component';
import { DxTabPanelModule, DxCheckBoxModule, DxTemplateModule } from 'devextreme-angular';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [DxTabsModule,

    DxSelectBoxModule,
    DxMultiViewModule, TicketsComponent, ShiftsComponent, DxTabPanelModule, CommonModule
  ],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.scss'
})
export class ReportsComponent {
  isTabsReady = true; 

  selectedTabIndex = 0; 


  onTitleClick(event: any) {


    console.log('Tab clicked:', event);

  }

}

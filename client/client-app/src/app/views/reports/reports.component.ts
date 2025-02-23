import { Component } from '@angular/core';
import { DxTabsModule, DxSelectBoxModule, DxMultiViewModule } from 'devextreme-angular';
import { TicketsComponent } from '../tickets/tickets.component';
import { ShiftsComponent } from '../shifts/shifts.component';
import { DxTabPanelModule, DxCheckBoxModule, DxTemplateModule } from 'devextreme-angular';
import { CommonModule } from '@angular/common';
import { ShiftsReportComponent } from './shifts-report/shifts-report.component';
import { TicketsReportComponent } from './tickets-report/tickets-report.component';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [DxTabsModule,
    TicketsReportComponent,
    ShiftsReportComponent,
    DxSelectBoxModule,
    DxMultiViewModule, TicketsComponent, ShiftsComponent, DxTabPanelModule, CommonModule
  ],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.scss'
})
export class ReportsComponent {
  selectedTabIndex: number;
  selectedTabName: any;
  constructor() {
    this.selectedTabIndex = 0;
  }
  ngOnInit(): void {
  }
  onTitleClick(e: any) {
    this.selectedTabIndex = e.itemIndex;
    this.selectedTabName = e.itemData.name;
    // Load data for the selected tab
  }

}

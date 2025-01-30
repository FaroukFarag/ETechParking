import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-side-menu',
  imports: [
    MenubarModule
  ],
  templateUrl: './side-menu.component.html',
  styleUrl: './side-menu.component.css'
})
export class SideMenuComponent implements OnInit {
  items: MenuItem[];

  constructor() {
    this.items = [
      {
        label: 'Locations',
        items: [
          {
            label: 'Locations',
            routerLink: 'locations',
          },
          {
            label: 'Fares',
            routerLink: 'locations/fares'
          }
        ]
      },
    ];
  }

  ngOnInit(): void {
  }
}

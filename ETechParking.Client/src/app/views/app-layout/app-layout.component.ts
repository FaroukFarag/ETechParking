import { Component, ViewChild } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { ContentComponent } from './content/content.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { Router } from '@angular/router';
import { DxDrawerModule, DxDrawerComponent, DxDrawerTypes } from 'devextreme-angular/ui/drawer';
import { DxListModule, DxToolbarModule } from 'devextreme-angular';

@Component({
  selector: 'app-app-layout',
  imports: [
    DxDrawerModule,
    HeaderComponent,
    ContentComponent,
    FooterComponent,
    SidebarComponent,
    DxToolbarModule,
    DxListModule,
  ],
  templateUrl: './app-layout.component.html',
  styleUrl: './app-layout.component.css'
})
export class AppLayoutComponent {
  isClosed = false;
  toggleSidebar() {
    this.isClosed = !this.isClosed;
  }

  selectedOpenMode: DxDrawerTypes.OpenedStateMode = 'shrink';
  selectedPosition: DxDrawerTypes.PanelLocation = 'left';
  selectedRevealMode: DxDrawerTypes.RevealMode = 'slide';

  isDrawerOpen = true;
  navigation: any = [
    { id: 1, text: 'Locations', icon: 'map' },
    { id: 2, text: 'Fares', icon: 'money', path: 'locations' },
    { id: 3, text: 'Users', icon: 'group' },

  ];
  toolbarContent = [{
    widget: 'dxButton',
    location: 'before',
    options: {
      icon: 'menu',
      stylingMode: 'text',
      onClick: () => this.isDrawerOpen = !this.isDrawerOpen,
    },
  },
  {
    widget: 'dxButton',
    location: 'after',
    options: {
      text: 'Sign Out',
      onClick: () => {
        console.log('Signout clicked');
      },
    },
    },
    {
      location: 'center',
      template: () => {
        const logo = document.createElement('img');
        logo.src = '/assets/images/EP-Logo.svg'; 
        logo.alt = 'Logo';
        logo.style.height = '50px'; 
        logo.style.marginRight = '100px'; 
        return logo;
      },
    },
  ];

  constructor(private router: Router) { }

  navigateTo(route: string) {
    this.router.navigate([route]);
  }
}

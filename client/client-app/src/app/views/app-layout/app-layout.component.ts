import { Component, ViewChild } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { ContentComponent } from './content/content.component';
import { FooterComponent } from './footer/footer.component';
import { Router } from '@angular/router';
import { DxDrawerModule, DxDrawerComponent, DxDrawerTypes } from 'devextreme-angular/ui/drawer';
import { DxListModule, DxToolbarModule } from 'devextreme-angular';
import { LoginComponent } from '../login/login.component';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-app-layout',
  standalone: true,
  imports: [
    DxDrawerModule,
    HeaderComponent,
    ContentComponent,
    FooterComponent,
    DxToolbarModule,
    DxListModule,
    LoginComponent,
    CommonModule
  ],
  templateUrl: './app-layout.component.html',
  styleUrl: './app-layout.component.scss'
})
export class AppLayoutComponent {
  canAccessMainLayout: boolean = true;
  isClosed = false;
  toggleSidebar() {
    this.isClosed = !this.isClosed;
  }

  selectedOpenMode: DxDrawerTypes.OpenedStateMode = 'shrink';
  selectedPosition: DxDrawerTypes.PanelLocation = 'left';
  selectedRevealMode: DxDrawerTypes.RevealMode = 'slide';

  isDrawerOpen = true;
 
  navigation: any = [
    { id: 1, text: 'Locations', icon: '/assets/icons/location.svg'},
    { id: 2, text: 'Fares', icon: '/assets/icons/fare.svg'},
    { id: 3, text: 'Tickets', icon: '/assets/icons/tickets.svg'},
    { id: 3, text: 'Shifts', icon: '/assets/icons/shift.svg'},
    { id: 4, text: 'Users', icon: '/assets/icons/users.svg' },
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
  //{
  //  widget: 'dxButton',
  //  location: 'after',
  //  class:'signOutBtn',
  //  options: {
  //    text: 'Sign Out',
  //    onClick: () => {
  //      this.canAccessMainLayout = false;
  //      this.router.navigate(['/login']);
  //    },
  //  },
  //  },
    //{
    //  location: 'center',
    //  template: () => {
    //    const logo = document.createElement('img');
    //    logo.src = '/assets/images/EP-Logo.svg'; 
    //    logo.alt = 'Logo';
    //    logo.style.height = '72px'; 
    //    //logo.style.marginRight = '100px'; 
    //    return logo;
    //  },
    //},
  ];

  constructor(private router: Router) { }

  navigateTo(route: string) {
    this.router.navigate([route]);
  }
}

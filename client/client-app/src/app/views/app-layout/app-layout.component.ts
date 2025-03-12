import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { ContentComponent } from './content/content.component';
import { FooterComponent } from './footer/footer.component';
import { Router } from '@angular/router';
import { DxDrawerModule, DxDrawerComponent, DxDrawerTypes } from 'devextreme-angular/ui/drawer';
import { DxListModule, DxToolbarModule } from 'devextreme-angular';
import { LoginComponent } from '../login/login.component';
import { CommonModule } from '@angular/common';
import { ResetPasswordComponent } from '../reset-password/reset-password.component'; // Adjust the path as necessary
import { AuthService } from '../../services/auth/auth.service';
import { RoleService } from '../../services/role/role.service';
@Component({
  selector: 'app-app-layout',
  standalone: true,
  imports: [
    DxDrawerModule,
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
export class AppLayoutComponent implements OnInit {
  canAccessMainLayout: boolean = false;
  isFirstLogin: boolean = false;
  isClosed = false;
  toggleSidebar() {
    this.isClosed = !this.isClosed;
  }

  selectedOpenMode: DxDrawerTypes.OpenedStateMode = 'shrink';
  selectedPosition: DxDrawerTypes.PanelLocation = 'left';
  selectedRevealMode: DxDrawerTypes.RevealMode = 'slide';
  isDrawerOpen = true;
  navigation: any = [];
  initializeNavigation() {
    if (this.roleService.isAdmin()) {
      this.navigation = [
        { id: 1, text: 'Dashboard', icon: '/assets/icons/location.svg' },
        { id: 2, text: 'Locations', icon: '/assets/icons/location.svg' },
        { id: 3, text: 'Rates', icon: '/assets/icons/fare.svg' },
        { id: 4, text: 'Tickets', icon: '/assets/icons/tickets.svg' },
        { id: 5, text: 'Shifts', icon: '/assets/icons/shift.svg' },
        { id: 6, text: 'Users', icon: '/assets/icons/users.svg' },
        { id: 7, text: 'Reports', icon: '/assets/icons/reports.svg' },
      ];
    } else if (this.roleService.isCashier()) {
      this.navigation = [
        { id: 3, text: 'Tickets', icon: '/assets/icons/tickets.svg' },
        { id: 4, text: 'Shifts', icon: '/assets/icons/shift.svg' },
      ];
    }
    else if (this.roleService.isAccountant()) {

      [
        { id: 3, text: 'Tickets', icon: '/assets/icons/tickets.svg' },
        { id: 4, text: 'Shifts', icon: '/assets/icons/shift.svg' },
      ];

    }

  }
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
      icon: 'login',
      stylingMode: 'text',
      text: 'Log out',
      onClick: () => this.authService.logout(),
    },
  },
  ];

  constructor(private router: Router, private authService: AuthService, private roleService: RoleService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') && !this.authService.isTokenExpired(localStorage.getItem('token')))
      this.canAccessMainLayout = true;
  }

  isAdmin(): boolean {
    return this.roleService.isAdmin();
  }
  isAccountant(): boolean {
    return this.roleService.isAccountant();
  }
  isCashier(): boolean {
    return this.roleService.isCashier();
  }
  navigateTo(route: string) {
    this.router.navigate([route]);
  }


  ngAfterViewInit() {

    // Call initializeNavigation after a delay of 1000 milliseconds (1 second)

    setTimeout(() => {

      this.initializeNavigation();

    }, 1000); // Adjust the delay as needed

  }

  onLoginClicked() {
    this.canAccessMainLayout = true;
  }
}

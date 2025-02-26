import { Routes } from '@angular/router';
import { Locationsv2Component } from './views/locationsv2/locationsv2.component';
import { UsersComponent } from './views/users/users.component';
import { FaresComponent } from './views/fares/fares.component';
import { LoginComponent } from './views/login/login.component';
import { TicketsComponent } from './views/tickets/tickets.component';
import { ShiftsComponent } from './views/shifts/shifts.component';
import { ReportsComponent } from './views/reports/reports.component';
import { ResetPasswordComponent } from './views/reset-password/reset-password.component';

export const routes: Routes = [
  //  {
  //      path: 'locations',
  //      loadChildren: () => import('./views/locations/locations.route').then(l => l.routes)
  //}, 
  {
    path: 'locations',
    component: Locationsv2Component
  },
  {
    path: 'users',
    component: UsersComponent
  },
  {
    path: 'rates',
    component: FaresComponent
  },
  {
    path: 'tickets',
    component: TicketsComponent
  },
    {
      path: 'shifts',
      component: ShiftsComponent
  },
  {
    path: 'reports',
    component: ReportsComponent
  },


  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'reset-password',
    component: ResetPasswordComponent
  },
  {
    path: 'locations',
    redirectTo: '/locations',
    pathMatch: 'full'
  }, 
  {
    path: '**',
    redirectTo: '/locations'
  }
];

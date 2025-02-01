import { Routes } from '@angular/router';
import { LocationsComponent } from './locations.component';
import { FaresComponent } from './fares/fares.component';
import { UsersComponent } from './users/users.component';

export const routes: Routes = [
    {
        path: '',
        component: LocationsComponent
    },
    {
        path: 'users',
        component: UsersComponent
    },
    {
        path: 'fares',
        component: FaresComponent
    }
];

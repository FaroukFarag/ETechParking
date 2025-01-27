import { Routes } from '@angular/router';
import { LocationsComponent } from './locations.component';
import { FaresComponent } from './fares/fares.component';

export const routes: Routes = [
    {
        path: '',
        component: LocationsComponent
    },
    {
        path: 'fares',
        component: FaresComponent
    }
];

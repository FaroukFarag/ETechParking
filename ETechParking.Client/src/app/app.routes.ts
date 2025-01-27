import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'locations',
        loadChildren: () => import('./views/locations/locations.route').then(l => l.routes)
    }
];

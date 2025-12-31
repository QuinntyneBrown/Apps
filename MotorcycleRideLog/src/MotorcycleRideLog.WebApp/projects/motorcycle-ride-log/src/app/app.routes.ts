import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'motorcycles',
    loadComponent: () => import('./pages').then(m => m.Motorcycles)
  },
  {
    path: 'rides',
    loadComponent: () => import('./pages').then(m => m.Rides)
  },
  {
    path: 'maintenance',
    loadComponent: () => import('./pages').then(m => m.MaintenancePage)
  },
  {
    path: 'routes',
    loadComponent: () => import('./pages').then(m => m.RoutesPage)
  }
];

import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'wines',
    loadComponent: () => import('./pages/wines/wines').then(m => m.Wines)
  },
  {
    path: 'drinking-windows',
    loadComponent: () => import('./pages/drinking-windows/drinking-windows').then(m => m.DrinkingWindows)
  }
];

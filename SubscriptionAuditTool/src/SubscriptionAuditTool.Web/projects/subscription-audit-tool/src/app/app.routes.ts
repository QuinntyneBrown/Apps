import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'categories',
    loadComponent: () => import('@lib/categories').then(m => m.Categories)
  }
];

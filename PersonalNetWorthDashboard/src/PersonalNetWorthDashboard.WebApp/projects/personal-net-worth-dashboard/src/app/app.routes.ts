import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'assets',
    loadComponent: () => import('./pages/assets').then(m => m.Assets)
  },
  {
    path: 'liabilities',
    loadComponent: () => import('./pages/liabilities').then(m => m.Liabilities)
  },
  {
    path: 'snapshots',
    loadComponent: () => import('./pages/snapshots').then(m => m.Snapshots)
  }
];

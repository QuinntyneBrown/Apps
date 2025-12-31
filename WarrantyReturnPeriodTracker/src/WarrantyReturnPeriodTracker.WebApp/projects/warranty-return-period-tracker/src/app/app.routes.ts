import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'purchases',
    loadComponent: () => import('./pages/purchases/purchases').then(m => m.Purchases)
  },
  {
    path: 'receipts',
    loadComponent: () => import('./pages/receipts/receipts').then(m => m.Receipts)
  },
  {
    path: 'return-windows',
    loadComponent: () => import('./pages/return-windows/return-windows').then(m => m.ReturnWindows)
  },
  {
    path: 'warranties',
    loadComponent: () => import('./pages/warranties/warranties').then(m => m.Warranties)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

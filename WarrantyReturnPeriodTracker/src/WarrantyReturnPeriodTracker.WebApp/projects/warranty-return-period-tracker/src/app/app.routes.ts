import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'purchases',
    loadComponent: () => import('@lib/purchases').then(m => m.Purchases)
  },
  {
    path: 'receipts',
    loadComponent: () => import('@lib/receipts').then(m => m.Receipts)
  },
  {
    path: 'return-windows',
    loadComponent: () => import('@lib/return-windows').then(m => m.ReturnWindows)
  },
  {
    path: 'warranties',
    loadComponent: () => import('@lib/warranties').then(m => m.Warranties)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

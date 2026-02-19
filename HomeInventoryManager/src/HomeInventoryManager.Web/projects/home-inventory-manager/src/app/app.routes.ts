import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'items',
    loadComponent: () => import('@lib/items').then(m => m.Items)
  },
  {
    path: 'value-estimates',
    loadComponent: () => import('@lib/value-estimates').then(m => m.ValueEstimates)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

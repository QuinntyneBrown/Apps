import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'items',
    loadComponent: () => import('./pages/items/items').then(m => m.Items)
  },
  {
    path: 'value-estimates',
    loadComponent: () => import('./pages/value-estimates/value-estimates').then(m => m.ValueEstimates)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

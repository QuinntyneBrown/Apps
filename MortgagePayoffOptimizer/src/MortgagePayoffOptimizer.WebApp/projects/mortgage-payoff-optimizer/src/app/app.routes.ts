import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'mortgages',
    loadComponent: () => import('./pages/mortgages').then(m => m.Mortgages)
  },
  {
    path: 'payments',
    loadComponent: () => import('./pages/payments').then(m => m.Payments)
  },
  {
    path: 'refinance-scenarios',
    loadComponent: () => import('./pages/refinance-scenarios').then(m => m.RefinanceScenarios)
  }
];

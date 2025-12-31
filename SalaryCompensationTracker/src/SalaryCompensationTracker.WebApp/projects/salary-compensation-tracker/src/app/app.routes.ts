import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'compensations',
    loadComponent: () => import('./pages/compensations/compensations').then(m => m.Compensations)
  },
  {
    path: 'benefits',
    loadComponent: () => import('./pages/benefits/benefits').then(m => m.Benefits)
  },
  {
    path: 'market-comparisons',
    loadComponent: () => import('./pages/market-comparisons/market-comparisons').then(m => m.MarketComparisons)
  }
];

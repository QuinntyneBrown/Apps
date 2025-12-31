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
    path: 'vehicles',
    loadComponent: () => import('./pages/vehicles/vehicles').then(m => m.Vehicles)
  },
  {
    path: 'value-assessments',
    loadComponent: () => import('./pages/value-assessments/value-assessments').then(m => m.ValueAssessments)
  },
  {
    path: 'market-comparisons',
    loadComponent: () => import('./pages/market-comparisons/market-comparisons').then(m => m.MarketComparisons)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];

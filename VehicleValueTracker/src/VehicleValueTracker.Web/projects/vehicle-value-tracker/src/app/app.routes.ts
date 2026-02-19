import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'vehicles',
    loadComponent: () => import('@lib/vehicles').then(m => m.Vehicles)
  },
  {
    path: 'value-assessments',
    loadComponent: () => import('@lib/value-assessments').then(m => m.ValueAssessments)
  },
  {
    path: 'market-comparisons',
    loadComponent: () => import('@lib/market-comparisons').then(m => m.MarketComparisons)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];

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
    path: 'businesses',
    loadComponent: () => import('@lib/businesses').then(m => m.Businesses)
  },
  {
    path: 'incomes',
    loadComponent: () => import('@lib/incomes').then(m => m.Incomes)
  },
  {
    path: 'expenses',
    loadComponent: () => import('@lib/expenses').then(m => m.Expenses)
  },
  {
    path: 'tax-estimates',
    loadComponent: () => import('@lib/tax-estimates').then(m => m.TaxEstimates)
  }
];

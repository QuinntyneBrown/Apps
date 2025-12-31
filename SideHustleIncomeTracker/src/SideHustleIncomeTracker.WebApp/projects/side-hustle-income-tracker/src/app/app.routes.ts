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
    path: 'businesses',
    loadComponent: () => import('./pages/businesses/businesses').then(m => m.Businesses)
  },
  {
    path: 'incomes',
    loadComponent: () => import('./pages/incomes/incomes').then(m => m.Incomes)
  },
  {
    path: 'expenses',
    loadComponent: () => import('./pages/expenses/expenses').then(m => m.Expenses)
  },
  {
    path: 'tax-estimates',
    loadComponent: () => import('./pages/tax-estimates/tax-estimates').then(m => m.TaxEstimates)
  }
];

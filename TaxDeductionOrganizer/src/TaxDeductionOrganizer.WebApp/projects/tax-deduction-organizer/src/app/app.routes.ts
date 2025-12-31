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
    path: 'tax-years',
    loadComponent: () => import('./pages/tax-years/tax-years').then(m => m.TaxYears)
  },
  {
    path: 'deductions',
    loadComponent: () => import('./pages/deductions/deductions').then(m => m.Deductions)
  }
];

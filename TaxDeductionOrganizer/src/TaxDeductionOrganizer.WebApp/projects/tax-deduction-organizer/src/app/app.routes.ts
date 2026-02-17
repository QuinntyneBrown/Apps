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
    path: 'tax-years',
    loadComponent: () => import('@lib/tax-years').then(m => m.TaxYears)
  },
  {
    path: 'deductions',
    loadComponent: () => import('@lib/deductions').then(m => m.Deductions)
  }
];

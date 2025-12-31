import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'properties',
    loadComponent: () => import('./pages').then(m => m.PropertiesList)
  },
  {
    path: 'properties/new',
    loadComponent: () => import('./pages').then(m => m.PropertyForm)
  },
  {
    path: 'properties/:id',
    loadComponent: () => import('./pages').then(m => m.PropertyForm)
  },
  {
    path: 'leases',
    loadComponent: () => import('./pages').then(m => m.LeasesList)
  },
  {
    path: 'leases/new',
    loadComponent: () => import('./pages').then(m => m.LeaseForm)
  },
  {
    path: 'leases/:id',
    loadComponent: () => import('./pages').then(m => m.LeaseForm)
  },
  {
    path: 'expenses',
    loadComponent: () => import('./pages').then(m => m.ExpensesList)
  },
  {
    path: 'expenses/new',
    loadComponent: () => import('./pages').then(m => m.ExpenseForm)
  },
  {
    path: 'expenses/:id',
    loadComponent: () => import('./pages').then(m => m.ExpenseForm)
  },
  {
    path: 'cash-flows',
    loadComponent: () => import('./pages').then(m => m.CashFlowsList)
  },
  {
    path: 'cash-flows/new',
    loadComponent: () => import('./pages').then(m => m.CashFlowForm)
  },
  {
    path: 'cash-flows/:id',
    loadComponent: () => import('./pages').then(m => m.CashFlowForm)
  }
];

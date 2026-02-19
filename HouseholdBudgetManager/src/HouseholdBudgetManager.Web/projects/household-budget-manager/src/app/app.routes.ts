import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard) },
  { path: 'budgets', loadComponent: () => import('./pages/budgets').then(m => m.Budgets) },
  { path: 'expenses', loadComponent: () => import('./pages/expenses').then(m => m.Expenses) },
  { path: 'incomes', loadComponent: () => import('./pages/incomes').then(m => m.Incomes) },
  { path: '**', redirectTo: 'dashboard' }
];

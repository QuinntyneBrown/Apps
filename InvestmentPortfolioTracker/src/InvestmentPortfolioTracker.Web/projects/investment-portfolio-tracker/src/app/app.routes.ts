import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', loadComponent: () => import('./pages').then(m => m.Dashboard) },
  { path: 'accounts', loadComponent: () => import('./pages').then(m => m.Accounts) },
  { path: 'holdings', loadComponent: () => import('./pages').then(m => m.Holdings) },
  { path: 'transactions', loadComponent: () => import('./pages').then(m => m.Transactions) }
];

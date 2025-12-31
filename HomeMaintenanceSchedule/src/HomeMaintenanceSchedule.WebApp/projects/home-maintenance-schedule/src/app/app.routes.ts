import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard) },
  { path: 'tasks', loadComponent: () => import('./pages/tasks').then(m => m.Tasks) },
  { path: 'contractors', loadComponent: () => import('./pages/contractors').then(m => m.Contractors) },
  { path: 'service-logs', loadComponent: () => import('./pages/service-logs').then(m => m.ServiceLogs) },
  { path: '**', redirectTo: 'dashboard' }
];

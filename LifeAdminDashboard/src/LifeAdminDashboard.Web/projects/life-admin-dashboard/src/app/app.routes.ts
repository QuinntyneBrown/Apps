import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'admin-tasks',
    loadComponent: () => import('./pages/admin-tasks').then(m => m.AdminTasks)
  },
  {
    path: 'deadlines',
    loadComponent: () => import('./pages/deadlines').then(m => m.Deadlines)
  },
  {
    path: 'renewals',
    loadComponent: () => import('./pages/renewals').then(m => m.Renewals)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

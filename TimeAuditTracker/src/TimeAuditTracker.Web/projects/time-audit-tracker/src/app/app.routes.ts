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
    path: 'time-blocks',
    loadComponent: () => import('@lib/time-blocks').then(m => m.TimeBlocks)
  },
  {
    path: 'goals',
    loadComponent: () => import('@lib/goals').then(m => m.Goals)
  },
  {
    path: 'audit-reports',
    loadComponent: () => import('@lib/audit-reports').then(m => m.AuditReports)
  }
];

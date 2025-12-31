import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'time-blocks',
    loadComponent: () => import('./pages/time-blocks/time-blocks').then(m => m.TimeBlocks)
  },
  {
    path: 'goals',
    loadComponent: () => import('./pages/goals/goals').then(m => m.Goals)
  },
  {
    path: 'audit-reports',
    loadComponent: () => import('./pages/audit-reports/audit-reports').then(m => m.AuditReports)
  }
];

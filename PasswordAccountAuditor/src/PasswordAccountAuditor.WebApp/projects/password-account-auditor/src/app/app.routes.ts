import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'accounts',
    loadComponent: () => import('./pages/accounts').then(m => m.Accounts)
  },
  {
    path: 'breach-alerts',
    loadComponent: () => import('./pages/breach-alerts').then(m => m.BreachAlerts)
  },
  {
    path: 'security-audits',
    loadComponent: () => import('./pages/security-audits').then(m => m.SecurityAudits)
  }
];

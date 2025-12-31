import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'companies',
    loadComponent: () => import('../pages/companies').then(m => m.Companies)
  },
  {
    path: 'applications',
    loadComponent: () => import('../pages/applications').then(m => m.Applications)
  },
  {
    path: 'interviews',
    loadComponent: () => import('../pages/interviews').then(m => m.Interviews)
  }
];

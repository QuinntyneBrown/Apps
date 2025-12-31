import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../../../pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'albums',
    loadComponent: () => import('../../../pages/albums').then(m => m.Albums)
  },
  {
    path: 'artists',
    loadComponent: () => import('../../../pages/artists').then(m => m.Artists)
  },
  {
    path: 'listening-logs',
    loadComponent: () => import('../../../pages/listening-logs').then(m => m.ListeningLogs)
  }
];

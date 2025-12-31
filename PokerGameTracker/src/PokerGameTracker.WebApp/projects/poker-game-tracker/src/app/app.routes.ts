import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'sessions',
    loadComponent: () => import('./pages').then(m => m.Sessions)
  },
  {
    path: 'hands',
    loadComponent: () => import('./pages').then(m => m.Hands)
  },
  {
    path: 'bankrolls',
    loadComponent: () => import('./pages').then(m => m.Bankrolls)
  }
];

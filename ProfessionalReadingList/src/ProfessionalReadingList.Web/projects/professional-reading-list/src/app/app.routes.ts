import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'resources',
    loadComponent: () => import('./pages').then(m => m.Resources)
  },
  {
    path: 'notes',
    loadComponent: () => import('./pages').then(m => m.Notes)
  },
  {
    path: 'progress',
    loadComponent: () => import('./pages').then(m => m.ReadingProgressPage)
  }
];

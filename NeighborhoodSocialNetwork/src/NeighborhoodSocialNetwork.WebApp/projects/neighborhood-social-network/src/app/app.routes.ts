import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../pages').then(m => m.Dashboard)
  },
  {
    path: 'neighbors',
    loadComponent: () => import('../pages').then(m => m.Neighbors)
  },
  {
    path: 'events',
    loadComponent: () => import('../pages').then(m => m.Events)
  },
  {
    path: 'messages',
    loadComponent: () => import('../pages').then(m => m.Messages)
  },
  {
    path: 'recommendations',
    loadComponent: () => import('../pages').then(m => m.Recommendations)
  }
];

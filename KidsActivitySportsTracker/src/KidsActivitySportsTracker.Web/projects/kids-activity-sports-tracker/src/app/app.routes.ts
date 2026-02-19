import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../pages').then(m => m.Dashboard)
  },
  {
    path: 'activities',
    loadComponent: () => import('../pages').then(m => m.Activities)
  },
  {
    path: 'carpools',
    loadComponent: () => import('../pages').then(m => m.Carpools)
  },
  {
    path: 'schedules',
    loadComponent: () => import('../pages').then(m => m.Schedules)
  }
];

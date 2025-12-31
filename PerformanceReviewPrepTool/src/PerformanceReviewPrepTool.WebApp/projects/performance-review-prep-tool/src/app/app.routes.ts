import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'review-periods',
    loadComponent: () => import('./pages').then(m => m.ReviewPeriods)
  },
  {
    path: 'achievements',
    loadComponent: () => import('./pages').then(m => m.Achievements)
  },
  {
    path: 'goals',
    loadComponent: () => import('./pages').then(m => m.Goals)
  },
  {
    path: 'feedbacks',
    loadComponent: () => import('./pages').then(m => m.Feedbacks)
  }
];

import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'mission-statements',
    loadComponent: () => import('./pages/mission-statements').then(m => m.MissionStatements)
  },
  {
    path: 'values',
    loadComponent: () => import('./pages/values').then(m => m.Values)
  },
  {
    path: 'goals',
    loadComponent: () => import('./pages/goals').then(m => m.Goals)
  },
  {
    path: 'progress',
    loadComponent: () => import('./pages/progress').then(m => m.ProgressPage)
  }
];

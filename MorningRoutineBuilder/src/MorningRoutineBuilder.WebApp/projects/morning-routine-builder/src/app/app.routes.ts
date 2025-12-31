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
    path: 'routines',
    loadComponent: () => import('./pages/routines').then(m => m.Routines)
  },
  {
    path: 'tasks',
    loadComponent: () => import('./pages/tasks').then(m => m.Tasks)
  },
  {
    path: 'completion-logs',
    loadComponent: () => import('./pages/completion-logs').then(m => m.CompletionLogs)
  },
  {
    path: 'streaks',
    loadComponent: () => import('./pages/streaks').then(m => m.Streaks)
  }
];

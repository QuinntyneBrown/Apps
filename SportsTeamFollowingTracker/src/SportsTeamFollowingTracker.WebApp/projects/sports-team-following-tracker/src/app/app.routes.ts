import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'teams',
    loadComponent: () => import('./pages/teams/teams').then(m => m.Teams)
  },
  {
    path: 'games',
    loadComponent: () => import('./pages/games/games').then(m => m.Games)
  },
  {
    path: 'seasons',
    loadComponent: () => import('./pages/seasons/seasons').then(m => m.Seasons)
  },
  {
    path: 'statistics',
    loadComponent: () => import('./pages/statistics/statistics').then(m => m.Statistics)
  }
];

import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'teams',
    loadComponent: () => import('@lib/teams').then(m => m.Teams)
  },
  {
    path: 'games',
    loadComponent: () => import('@lib/games').then(m => m.Games)
  },
  {
    path: 'seasons',
    loadComponent: () => import('@lib/seasons').then(m => m.Seasons)
  },
  {
    path: 'statistics',
    loadComponent: () => import('@lib/statistics').then(m => m.Statistics)
  }
];

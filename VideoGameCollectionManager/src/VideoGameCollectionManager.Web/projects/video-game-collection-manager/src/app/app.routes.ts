import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'games',
    loadComponent: () => import('@lib/games').then(m => m.Games)
  },
  {
    path: 'play-sessions',
    loadComponent: () => import('@lib/play-sessions').then(m => m.PlaySessions)
  },
  {
    path: 'wishlists',
    loadComponent: () => import('@lib/wishlists').then(m => m.Wishlists)
  }
];

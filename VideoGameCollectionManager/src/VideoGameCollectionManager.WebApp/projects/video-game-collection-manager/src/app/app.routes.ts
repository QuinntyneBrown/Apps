import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'games',
    loadComponent: () => import('./pages/games/games').then(m => m.Games)
  },
  {
    path: 'play-sessions',
    loadComponent: () => import('./pages/play-sessions/play-sessions').then(m => m.PlaySessions)
  },
  {
    path: 'wishlists',
    loadComponent: () => import('./pages/wishlists/wishlists').then(m => m.Wishlists)
  }
];

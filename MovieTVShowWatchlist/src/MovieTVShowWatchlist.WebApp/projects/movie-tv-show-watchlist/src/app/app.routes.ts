import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'watchlist', pathMatch: 'full' },
  {
    path: 'watchlist',
    loadComponent: () => import('./pages/watchlist').then(m => m.Watchlist)
  },
  {
    path: 'statistics',
    loadComponent: () => import('./pages/statistics').then(m => m.Statistics)
  },
  {
    path: 'history',
    loadComponent: () => import('./pages/history').then(m => m.History)
  },
  {
    path: 'discover',
    loadComponent: () => import('./pages/discover').then(m => m.Discover)
  },
  { path: '**', redirectTo: 'watchlist' }
];

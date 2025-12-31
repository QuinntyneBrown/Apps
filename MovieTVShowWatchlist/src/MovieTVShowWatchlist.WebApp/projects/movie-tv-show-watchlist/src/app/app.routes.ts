import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard-index').then(m => m.Dashboard)
  },
  {
    path: 'watchlist',
    loadComponent: () => import('./pages/watchlist').then(m => m.Watchlist)
  },
  {
    path: 'favorites',
    loadComponent: () => import('./pages/favorites').then(m => m.Favorites)
  },
  {
    path: 'ratings',
    loadComponent: () => import('./pages/ratings').then(m => m.Ratings)
  },
  {
    path: 'reviews',
    loadComponent: () => import('./pages/reviews').then(m => m.Reviews)
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
  { path: '**', redirectTo: 'dashboard' }
];

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
    path: 'books',
    loadComponent: () => import('./pages/books/books').then(m => m.Books)
  },
  {
    path: 'reading-logs',
    loadComponent: () => import('./pages/reading-logs/reading-logs').then(m => m.ReadingLogs)
  },
  {
    path: 'reviews',
    loadComponent: () => import('./pages/reviews/reviews').then(m => m.Reviews)
  },
  {
    path: 'wishlist',
    loadComponent: () => import('./pages/wishlist/wishlist').then(m => m.Wishlist)
  }
];

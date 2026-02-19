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
    path: 'books',
    loadComponent: () => import('@lib/books').then(m => m.Books)
  },
  {
    path: 'reading-logs',
    loadComponent: () => import('@lib/reading-logs').then(m => m.ReadingLogs)
  },
  {
    path: 'reviews',
    loadComponent: () => import('@lib/reviews').then(m => m.Reviews)
  },
  {
    path: 'wishlist',
    loadComponent: () => import('@lib/wishlist').then(m => m.Wishlist)
  }
];

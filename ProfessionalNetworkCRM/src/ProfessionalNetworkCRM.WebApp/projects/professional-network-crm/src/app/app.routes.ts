import { Routes } from '@angular/router';
import { authGuard } from './guards';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./pages').then(m => m.Login)
  },
  {
    path: '',
    loadComponent: () => import('./layouts').then(m => m.MainLayout),
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./pages').then(m => m.Dashboard)
      },
      {
        path: 'contacts',
        loadComponent: () => import('./pages').then(m => m.ContactsList)
      },
      {
        path: 'contacts/:id',
        loadComponent: () => import('./pages').then(m => m.ContactForm)
      },
      {
        path: 'follow-ups',
        loadComponent: () => import('./pages').then(m => m.FollowUpsList)
      },
      {
        path: 'follow-ups/:id',
        loadComponent: () => import('./pages').then(m => m.FollowUpForm)
      },
      {
        path: 'interactions',
        loadComponent: () => import('./pages').then(m => m.InteractionsList)
      },
      {
        path: 'interactions/:id',
        loadComponent: () => import('./pages').then(m => m.InteractionForm)
      }
    ]
  }
];

import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'dates',
    loadComponent: () => import('./pages/dates/date-list').then(m => m.DateList)
  },
  {
    path: 'dates/new',
    loadComponent: () => import('./pages/dates/date-form').then(m => m.DateForm)
  },
  {
    path: 'dates/:id/edit',
    loadComponent: () => import('./pages/dates/date-form').then(m => m.DateForm)
  },
  {
    path: 'reminders',
    loadComponent: () => import('./pages/reminders/reminders').then(m => m.Reminders)
  },
  {
    path: 'reminders/:dateId',
    loadComponent: () => import('./pages/reminders/reminders').then(m => m.Reminders)
  },
  {
    path: 'gifts',
    loadComponent: () => import('./pages/gifts/gift-list').then(m => m.GiftList)
  },
  {
    path: 'gifts/:dateId',
    loadComponent: () => import('./pages/gifts/gift-list').then(m => m.GiftList)
  },
  {
    path: 'celebrations',
    loadComponent: () => import('./pages/celebrations/celebration-list').then(m => m.CelebrationList)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

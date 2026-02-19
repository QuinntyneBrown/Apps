import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'dates',
    loadComponent: () => import('@lib/dates').then(m => m.DateList)
  },
  {
    path: 'dates/new',
    loadComponent: () => import('@lib/dates').then(m => m.DateForm)
  },
  {
    path: 'dates/:id/edit',
    loadComponent: () => import('@lib/dates').then(m => m.DateForm)
  },
  {
    path: 'reminders',
    loadComponent: () => import('@lib/reminders').then(m => m.Reminders)
  },
  {
    path: 'reminders/:dateId',
    loadComponent: () => import('@lib/reminders').then(m => m.Reminders)
  },
  {
    path: 'gifts',
    loadComponent: () => import('@lib/gifts').then(m => m.GiftList)
  },
  {
    path: 'gifts/:dateId',
    loadComponent: () => import('@lib/gifts').then(m => m.GiftList)
  },
  {
    path: 'celebrations',
    loadComponent: () => import('@lib/celebrations').then(m => m.CelebrationList)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

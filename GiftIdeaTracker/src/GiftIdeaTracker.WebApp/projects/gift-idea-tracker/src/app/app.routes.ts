import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'recipients',
    loadComponent: () => import('@lib/recipients').then(m => m.Recipients)
  },
  {
    path: 'gift-ideas',
    loadComponent: () => import('@lib/gift-ideas').then(m => m.GiftIdeas)
  },
  {
    path: 'purchases',
    loadComponent: () => import('@lib/purchases').then(m => m.Purchases)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

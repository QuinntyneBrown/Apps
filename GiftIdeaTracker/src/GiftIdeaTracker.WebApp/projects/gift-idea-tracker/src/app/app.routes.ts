import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'recipients',
    loadComponent: () => import('./pages/recipients/recipients').then(m => m.Recipients)
  },
  {
    path: 'gift-ideas',
    loadComponent: () => import('./pages/gift-ideas/gift-ideas').then(m => m.GiftIdeas)
  },
  {
    path: 'purchases',
    loadComponent: () => import('./pages/purchases/purchases').then(m => m.Purchases)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

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
        path: 'competitors',
        loadComponent: () => import('./pages').then(m => m.CompetitorsList)
      },
      {
        path: 'competitors/new',
        loadComponent: () => import('./pages').then(m => m.CompetitorForm)
      },
      {
        path: 'competitors/:id',
        loadComponent: () => import('./pages').then(m => m.CompetitorForm)
      },
      {
        path: 'insights',
        loadComponent: () => import('./pages').then(m => m.InsightsList)
      },
      {
        path: 'insights/new',
        loadComponent: () => import('./pages').then(m => m.InsightForm)
      },
      {
        path: 'insights/:id',
        loadComponent: () => import('./pages').then(m => m.InsightForm)
      },
      {
        path: 'alerts',
        loadComponent: () => import('./pages').then(m => m.AlertsList)
      },
      {
        path: 'alerts/new',
        loadComponent: () => import('./pages').then(m => m.AlertForm)
      },
      {
        path: 'alerts/:id',
        loadComponent: () => import('./pages').then(m => m.AlertForm)
      },
      {
        path: 'reports',
        loadComponent: () => import('./pages').then(m => m.Reports)
      }
    ]
  }
];

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
    path: 'runs',
    loadComponent: () => import('@lib/runs').then(m => m.Runs)
  },
  {
    path: 'races',
    loadComponent: () => import('@lib/races').then(m => m.Races)
  },
  {
    path: 'training-plans',
    loadComponent: () => import('@lib/training-plans').then(m => m.TrainingPlans)
  }
];

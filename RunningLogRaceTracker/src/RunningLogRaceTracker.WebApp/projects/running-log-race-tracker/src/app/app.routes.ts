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
    path: 'runs',
    loadComponent: () => import('./pages/runs/runs').then(m => m.Runs)
  },
  {
    path: 'races',
    loadComponent: () => import('./pages/races/races').then(m => m.Races)
  },
  {
    path: 'training-plans',
    loadComponent: () => import('./pages/training-plans/training-plans').then(m => m.TrainingPlans)
  }
];

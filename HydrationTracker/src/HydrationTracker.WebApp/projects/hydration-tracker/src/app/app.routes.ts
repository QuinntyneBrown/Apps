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
    path: 'intakes',
    loadComponent: () => import('@lib/intakes').then(m => m.Intakes)
  },
  {
    path: 'goals',
    loadComponent: () => import('@lib/goals').then(m => m.Goals)
  },
  {
    path: 'reminders',
    loadComponent: () => import('@lib/reminders').then(m => m.Reminders)
  }
];

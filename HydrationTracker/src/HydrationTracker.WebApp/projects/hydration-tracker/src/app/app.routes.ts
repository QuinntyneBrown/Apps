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
    path: 'intakes',
    loadComponent: () => import('./pages/intakes/intakes').then(m => m.Intakes)
  },
  {
    path: 'goals',
    loadComponent: () => import('./pages/goals/goals').then(m => m.Goals)
  },
  {
    path: 'reminders',
    loadComponent: () => import('./pages/reminders/reminders').then(m => m.Reminders)
  }
];

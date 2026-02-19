import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'screenings',
    loadComponent: () => import('./pages/screenings').then(m => m.Screenings)
  },
  {
    path: 'appointments',
    loadComponent: () => import('./pages/appointments').then(m => m.Appointments)
  },
  {
    path: 'reminders',
    loadComponent: () => import('./pages/reminders').then(m => m.Reminders)
  }
];

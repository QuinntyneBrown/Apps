import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'calendar',
    loadComponent: () => import('@lib/calendar').then(m => m.Calendar)
  },
  {
    path: 'family-members',
    loadComponent: () => import('@lib/family-members').then(m => m.FamilyMembers)
  },
  {
    path: 'reminders',
    loadComponent: () => import('@lib/reminders').then(m => m.Reminders)
  },
  {
    path: 'households',
    loadComponent: () => import('@lib/households').then(m => m.Households)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'calendar',
    loadComponent: () => import('./pages/calendar/calendar').then(m => m.Calendar)
  },
  {
    path: 'family-members',
    loadComponent: () => import('./pages/family-members/family-members').then(m => m.FamilyMembers)
  },
  {
    path: 'reminders',
    loadComponent: () => import('./pages/reminders/reminders').then(m => m.Reminders)
  },
  {
    path: 'households',
    loadComponent: () => import('./pages/households/households').then(m => m.Households)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

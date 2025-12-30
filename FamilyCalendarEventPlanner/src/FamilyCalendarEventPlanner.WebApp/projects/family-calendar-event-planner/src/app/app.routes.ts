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
    path: '**',
    redirectTo: ''
  }
];

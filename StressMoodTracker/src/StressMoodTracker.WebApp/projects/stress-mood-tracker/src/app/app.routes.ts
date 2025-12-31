import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'mood-entries',
    loadComponent: () => import('./pages/mood-entries/mood-entries').then(m => m.MoodEntries)
  },
  {
    path: 'journals',
    loadComponent: () => import('./pages/journals/journals').then(m => m.Journals)
  },
  {
    path: 'triggers',
    loadComponent: () => import('./pages/triggers/triggers').then(m => m.Triggers)
  }
];

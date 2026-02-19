import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'mood-entries',
    loadComponent: () => import('@lib/mood-entries').then(m => m.MoodEntries)
  },
  {
    path: 'journals',
    loadComponent: () => import('@lib/journals').then(m => m.Journals)
  },
  {
    path: 'triggers',
    loadComponent: () => import('@lib/triggers').then(m => m.Triggers)
  }
];

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
    path: 'journal-entries',
    loadComponent: () => import('@lib/journal-entries').then(m => m.JournalEntries)
  },
  {
    path: 'prompts',
    loadComponent: () => import('@lib/prompts').then(m => m.Prompts)
  },
  {
    path: 'tags',
    loadComponent: () => import('@lib/tags').then(m => m.Tags)
  }
];

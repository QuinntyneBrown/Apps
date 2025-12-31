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
    path: 'journal-entries',
    loadComponent: () => import('./pages/journal-entries/journal-entries').then(m => m.JournalEntries)
  },
  {
    path: 'prompts',
    loadComponent: () => import('./pages/prompts/prompts').then(m => m.Prompts)
  },
  {
    path: 'tags',
    loadComponent: () => import('./pages/tags/tags').then(m => m.Tags)
  }
];

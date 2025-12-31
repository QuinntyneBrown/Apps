import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('../pages').then(m => m.Dashboard)
  },
  {
    path: 'notes',
    loadComponent: () => import('../pages').then(m => m.Notes)
  },
  {
    path: 'tags',
    loadComponent: () => import('../pages').then(m => m.Tags)
  },
  {
    path: 'note-links',
    loadComponent: () => import('../pages').then(m => m.NoteLinks)
  },
  {
    path: 'search-queries',
    loadComponent: () => import('../pages').then(m => m.SearchQueries)
  }
];

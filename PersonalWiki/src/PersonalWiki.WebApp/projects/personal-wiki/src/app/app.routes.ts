import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'wiki-pages',
    loadComponent: () => import('./pages').then(m => m.WikiPages)
  },
  {
    path: 'wiki-categories',
    loadComponent: () => import('./pages').then(m => m.WikiCategories)
  },
  {
    path: 'page-revisions',
    loadComponent: () => import('./pages').then(m => m.PageRevisions)
  },
  {
    path: 'page-links',
    loadComponent: () => import('./pages').then(m => m.PageLinks)
  }
];

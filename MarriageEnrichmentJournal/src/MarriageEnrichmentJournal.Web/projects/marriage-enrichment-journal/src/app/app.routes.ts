import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'journal-entries',
    loadComponent: () => import('../pages/journal-entry-list').then(m => m.JournalEntryList)
  },
  {
    path: 'journal-entries/create',
    loadComponent: () => import('../pages/journal-entry-form').then(m => m.JournalEntryForm)
  },
  {
    path: 'journal-entries/edit/:id',
    loadComponent: () => import('../pages/journal-entry-form').then(m => m.JournalEntryForm)
  },
  {
    path: 'gratitudes',
    loadComponent: () => import('../pages/gratitude-list').then(m => m.GratitudeList)
  },
  {
    path: 'gratitudes/create',
    loadComponent: () => import('../pages/gratitude-form').then(m => m.GratitudeForm)
  },
  {
    path: 'gratitudes/edit/:id',
    loadComponent: () => import('../pages/gratitude-form').then(m => m.GratitudeForm)
  },
  {
    path: 'reflections',
    loadComponent: () => import('../pages/reflection-list').then(m => m.ReflectionList)
  },
  {
    path: 'reflections/create',
    loadComponent: () => import('../pages/reflection-form').then(m => m.ReflectionForm)
  },
  {
    path: 'reflections/edit/:id',
    loadComponent: () => import('../pages/reflection-form').then(m => m.ReflectionForm)
  }
];

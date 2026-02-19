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
    path: 'meetings',
    loadComponent: () => import('../pages').then(m => m.Meetings)
  },
  {
    path: 'meetings/:id',
    loadComponent: () => import('../pages').then(m => m.MeetingForm)
  },
  {
    path: 'notes',
    loadComponent: () => import('../pages').then(m => m.Notes)
  },
  {
    path: 'notes/:id',
    loadComponent: () => import('../pages').then(m => m.NoteForm)
  },
  {
    path: 'action-items',
    loadComponent: () => import('../pages').then(m => m.ActionItems)
  },
  {
    path: 'action-items/:id',
    loadComponent: () => import('../pages').then(m => m.ActionItemForm)
  }
];

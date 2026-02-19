import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'groups',
    loadComponent: () => import('./pages').then(m => m.GroupsList)
  },
  {
    path: 'groups/:id',
    loadComponent: () => import('./pages').then(m => m.GroupForm)
  },
  {
    path: 'meetings',
    loadComponent: () => import('./pages').then(m => m.MeetingsList)
  },
  {
    path: 'meetings/:id',
    loadComponent: () => import('./pages').then(m => m.MeetingForm)
  },
  {
    path: 'topics',
    loadComponent: () => import('./pages').then(m => m.TopicsList)
  },
  {
    path: 'topics/:id',
    loadComponent: () => import('./pages').then(m => m.TopicForm)
  },
  {
    path: 'resources',
    loadComponent: () => import('./pages').then(m => m.ResourcesList)
  },
  {
    path: 'resources/:id',
    loadComponent: () => import('./pages').then(m => m.ResourceForm)
  }
];

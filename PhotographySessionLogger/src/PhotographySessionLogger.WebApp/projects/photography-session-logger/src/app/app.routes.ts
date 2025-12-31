import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'sessions',
    loadComponent: () => import('./pages/sessions').then(m => m.Sessions)
  },
  {
    path: 'sessions/new',
    loadComponent: () => import('./pages/session-form').then(m => m.SessionForm)
  },
  {
    path: 'sessions/:id',
    loadComponent: () => import('./pages/session-form').then(m => m.SessionForm)
  },
  {
    path: 'photos',
    loadComponent: () => import('./pages/photos').then(m => m.Photos)
  },
  {
    path: 'photos/new',
    loadComponent: () => import('./pages/photo-form').then(m => m.PhotoForm)
  },
  {
    path: 'photos/:id',
    loadComponent: () => import('./pages/photo-form').then(m => m.PhotoForm)
  },
  {
    path: 'gears',
    loadComponent: () => import('./pages/gears').then(m => m.Gears)
  },
  {
    path: 'gears/new',
    loadComponent: () => import('./pages/gear-form').then(m => m.GearForm)
  },
  {
    path: 'gears/:id',
    loadComponent: () => import('./pages/gear-form').then(m => m.GearForm)
  },
  {
    path: 'projects',
    loadComponent: () => import('./pages/projects').then(m => m.Projects)
  },
  {
    path: 'projects/new',
    loadComponent: () => import('./pages/project-form').then(m => m.ProjectForm)
  },
  {
    path: 'projects/:id',
    loadComponent: () => import('./pages/project-form').then(m => m.ProjectForm)
  }
];

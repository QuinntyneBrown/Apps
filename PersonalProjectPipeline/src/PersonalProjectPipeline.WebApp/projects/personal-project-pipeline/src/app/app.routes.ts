import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'projects',
    loadComponent: () => import('./pages').then(m => m.Projects)
  },
  {
    path: 'projects/new',
    loadComponent: () => import('./pages').then(m => m.ProjectForm)
  },
  {
    path: 'projects/:id',
    loadComponent: () => import('./pages').then(m => m.ProjectForm)
  },
  {
    path: 'milestones',
    loadComponent: () => import('./pages').then(m => m.Milestones)
  },
  {
    path: 'milestones/new',
    loadComponent: () => import('./pages').then(m => m.MilestoneForm)
  },
  {
    path: 'milestones/:id',
    loadComponent: () => import('./pages').then(m => m.MilestoneForm)
  },
  {
    path: 'tasks',
    loadComponent: () => import('./pages').then(m => m.Tasks)
  },
  {
    path: 'tasks/new',
    loadComponent: () => import('./pages').then(m => m.TaskForm)
  },
  {
    path: 'tasks/:id',
    loadComponent: () => import('./pages').then(m => m.TaskForm)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

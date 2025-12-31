import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'projects',
    loadComponent: () => import('./pages/projects/projects').then(m => m.Projects)
  },
  {
    path: 'materials',
    loadComponent: () => import('./pages/materials/materials').then(m => m.Materials)
  },
  {
    path: 'tools',
    loadComponent: () => import('./pages/tools/tools').then(m => m.Tools)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

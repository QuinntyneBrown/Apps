import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('@lib/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'skills',
    loadComponent: () => import('@lib/skills').then(m => m.Skills)
  },
  {
    path: 'courses',
    loadComponent: () => import('@lib/courses').then(m => m.Courses)
  },
  {
    path: 'certifications',
    loadComponent: () => import('@lib/certifications').then(m => m.Certifications)
  },
  {
    path: 'learning-paths',
    loadComponent: () => import('@lib/learning-paths').then(m => m.LearningPaths)
  }
];

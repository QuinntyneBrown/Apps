import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'skills',
    loadComponent: () => import('./pages/skills/skills').then(m => m.Skills)
  },
  {
    path: 'courses',
    loadComponent: () => import('./pages/courses/courses').then(m => m.Courses)
  },
  {
    path: 'certifications',
    loadComponent: () => import('./pages/certifications/certifications').then(m => m.Certifications)
  },
  {
    path: 'learning-paths',
    loadComponent: () => import('./pages/learning-paths/learning-paths').then(m => m.LearningPaths)
  }
];

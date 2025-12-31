import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'lessons',
    loadComponent: () => import('./pages/lessons').then(m => m.Lessons)
  },
  {
    path: 'sources',
    loadComponent: () => import('./pages/sources').then(m => m.Sources)
  },
  {
    path: 'reminders',
    loadComponent: () => import('./pages/lesson-reminders').then(m => m.LessonReminders)
  }
];

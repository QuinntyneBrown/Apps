import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'achievements',
    loadComponent: () => import('./pages/achievements-list').then(m => m.AchievementsList)
  },
  {
    path: 'achievements/new',
    loadComponent: () => import('./pages/achievement-form').then(m => m.AchievementForm)
  },
  {
    path: 'achievements/:id',
    loadComponent: () => import('./pages/achievement-form').then(m => m.AchievementForm)
  },
  {
    path: 'projects',
    loadComponent: () => import('./pages/projects-list').then(m => m.ProjectsList)
  },
  {
    path: 'projects/new',
    loadComponent: () => import('./pages/project-form').then(m => m.ProjectForm)
  },
  {
    path: 'projects/:id',
    loadComponent: () => import('./pages/project-form').then(m => m.ProjectForm)
  },
  {
    path: 'skills',
    loadComponent: () => import('./pages/skills-list').then(m => m.SkillsList)
  },
  {
    path: 'skills/new',
    loadComponent: () => import('./pages/skill-form').then(m => m.SkillForm)
  },
  {
    path: 'skills/:id',
    loadComponent: () => import('./pages/skill-form').then(m => m.SkillForm)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];

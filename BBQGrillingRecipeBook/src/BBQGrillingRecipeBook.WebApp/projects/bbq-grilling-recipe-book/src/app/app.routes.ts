import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'recipes',
    loadComponent: () => import('./pages').then(m => m.Recipes)
  },
  {
    path: 'cook-sessions',
    loadComponent: () => import('./pages').then(m => m.CookSessions)
  },
  {
    path: 'techniques',
    loadComponent: () => import('./pages').then(m => m.Techniques)
  }
];

import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard) },
  { path: 'injuries', loadComponent: () => import('./pages/injuries').then(m => m.Injuries) },
  { path: 'exercises', loadComponent: () => import('./pages/exercises').then(m => m.Exercises) },
  { path: 'milestones', loadComponent: () => import('./pages/milestones').then(m => m.Milestones) },
  { path: '**', redirectTo: 'dashboard' }
];

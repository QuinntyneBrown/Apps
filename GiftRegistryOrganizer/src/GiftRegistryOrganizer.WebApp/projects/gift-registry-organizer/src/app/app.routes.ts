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
    path: 'registries',
    loadComponent: () => import('./pages/registries/registries').then(m => m.Registries)
  },
  {
    path: 'registry-items',
    loadComponent: () => import('./pages/registry-items/registry-items').then(m => m.RegistryItems)
  },
  {
    path: 'contributions',
    loadComponent: () => import('./pages/contributions/contributions').then(m => m.Contributions)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];

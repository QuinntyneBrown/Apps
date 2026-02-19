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
    path: 'registries',
    loadComponent: () => import('@lib/registries').then(m => m.Registries)
  },
  {
    path: 'registry-items',
    loadComponent: () => import('@lib/registry-items').then(m => m.RegistryItems)
  },
  {
    path: 'contributions',
    loadComponent: () => import('@lib/contributions').then(m => m.Contributions)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];

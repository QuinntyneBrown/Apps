import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../pages').then(m => m.Dashboard)
  },
  {
    path: 'vitals',
    loadComponent: () => import('../pages').then(m => m.VitalsList)
  },
  {
    path: 'vitals/new',
    loadComponent: () => import('../pages').then(m => m.VitalForm)
  },
  {
    path: 'vitals/edit/:id',
    loadComponent: () => import('../pages').then(m => m.VitalForm)
  },
  {
    path: 'health-trends',
    loadComponent: () => import('../pages').then(m => m.HealthTrendsList)
  },
  {
    path: 'health-trends/new',
    loadComponent: () => import('../pages').then(m => m.HealthTrendForm)
  },
  {
    path: 'health-trends/edit/:id',
    loadComponent: () => import('../pages').then(m => m.HealthTrendForm)
  },
  {
    path: 'wearable-data',
    loadComponent: () => import('../pages').then(m => m.WearableDataList)
  },
  {
    path: 'wearable-data/new',
    loadComponent: () => import('../pages').then(m => m.WearableDataForm)
  },
  {
    path: 'wearable-data/edit/:id',
    loadComponent: () => import('../pages').then(m => m.WearableDataForm)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

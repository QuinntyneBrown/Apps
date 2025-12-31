import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'vehicles',
    loadComponent: () => import('./pages').then(m => m.Vehicles)
  },
  {
    path: 'emergency-contacts',
    loadComponent: () => import('./pages').then(m => m.EmergencyContacts)
  },
  {
    path: 'insurance-infos',
    loadComponent: () => import('./pages').then(m => m.InsuranceInfos)
  },
  {
    path: 'policies',
    loadComponent: () => import('./pages').then(m => m.Policies)
  }
];

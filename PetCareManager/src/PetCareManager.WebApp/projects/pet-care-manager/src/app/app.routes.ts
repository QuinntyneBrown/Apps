import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard';
import { PetList } from './pages/pet-list/pet-list';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'pets', component: PetList },
  { path: 'appointments', loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard) },
  { path: 'medications', loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard) },
];

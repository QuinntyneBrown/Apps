import { Routes } from '@angular/router';
import { Dashboard, Sessions, Analytics, Distractions } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'sessions', component: Sessions },
  { path: 'analytics', component: Analytics },
  { path: 'distractions', component: Distractions }
];

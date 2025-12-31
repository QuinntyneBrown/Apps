import { Routes } from '@angular/router';
import { Dashboard, Trips, Spots, Catches } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'trips', component: Trips },
  { path: 'spots', component: Spots },
  { path: 'catches', component: Catches }
];

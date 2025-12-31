import { Routes } from '@angular/router';
import { Vehicles, FillUps, Trips, Reports } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/vehicles', pathMatch: 'full' },
  { path: 'vehicles', component: Vehicles },
  { path: 'fill-ups', component: FillUps },
  { path: 'trips', component: Trips },
  { path: 'reports', component: Reports }
];

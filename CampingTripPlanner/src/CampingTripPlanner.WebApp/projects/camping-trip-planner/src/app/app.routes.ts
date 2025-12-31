import { Routes } from '@angular/router';
import { Dashboard, Campsites, CampsiteDetail, Trips, TripDetail, GearChecklists } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'campsites', component: Campsites },
  { path: 'campsites/:id', component: CampsiteDetail },
  { path: 'trips', component: Trips },
  { path: 'trips/:id', component: TripDetail },
  { path: 'gear-checklists', component: GearChecklists },
  { path: '**', redirectTo: '/dashboard' }
];

import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Campsites } from '@lib/campsites';
import { CampsiteDetail } from '@lib/campsite-detail';
import { Trips } from '@lib/trips';
import { TripDetail } from '@lib/trip-detail';
import { GearChecklists } from '@lib/gear-checklists';

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

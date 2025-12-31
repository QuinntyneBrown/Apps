import { Routes } from '@angular/router';
import { Dashboard, Medications, Schedules, Refills } from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'medications', component: Medications },
  { path: 'schedules', component: Schedules },
  { path: 'refills', component: Refills },
  { path: '**', redirectTo: '' }
];

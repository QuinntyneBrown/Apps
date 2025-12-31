import { Routes } from '@angular/router';
import { Dashboard, Destinations, Trips, Memories } from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'destinations', component: Destinations },
  { path: 'trips', component: Trips },
  { path: 'memories', component: Memories },
  { path: '**', redirectTo: '' }
];

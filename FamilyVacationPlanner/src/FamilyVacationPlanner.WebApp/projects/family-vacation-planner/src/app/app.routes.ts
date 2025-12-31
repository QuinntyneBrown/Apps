import { Routes } from '@angular/router';
import { Trips, Bookings, Itineraries, PackingLists, Budgets } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/trips', pathMatch: 'full' },
  { path: 'trips', component: Trips },
  { path: 'bookings', component: Bookings },
  { path: 'itineraries', component: Itineraries },
  { path: 'packing-lists', component: PackingLists },
  { path: 'budgets', component: Budgets }
];

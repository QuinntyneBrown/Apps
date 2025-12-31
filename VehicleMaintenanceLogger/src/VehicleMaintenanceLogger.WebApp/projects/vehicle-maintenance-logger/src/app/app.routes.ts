import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { Vehicles } from './pages/vehicles';
import { VehicleDetails } from './pages/vehicle-details';

export const routes: Routes = [
  { path: '', component: Dashboard, pathMatch: 'full' },
  { path: 'vehicles', component: Vehicles },
  { path: 'vehicles/:id', component: VehicleDetails },
  { path: '**', redirectTo: '' }
];

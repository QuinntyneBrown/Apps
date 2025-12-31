import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { Donations } from './pages/donations';
import { Organizations } from './pages/organizations';
import { TaxReports } from './pages/tax-reports';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'donations', component: Donations },
  { path: 'organizations', component: Organizations },
  { path: 'tax-reports', component: TaxReports }
];

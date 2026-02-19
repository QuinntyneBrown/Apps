import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Donations } from '@lib/donations';
import { Organizations } from '@lib/organizations';
import { TaxReports } from '@lib/tax-reports';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'donations', component: Donations },
  { path: 'organizations', component: Organizations },
  { path: 'tax-reports', component: TaxReports }
];

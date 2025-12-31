import { Routes } from '@angular/router';
import { Dashboard, Bills, Payees, Payments } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'bills', component: Bills },
  { path: 'payees', component: Payees },
  { path: 'payments', component: Payments },
];

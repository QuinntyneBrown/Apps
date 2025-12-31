import { Routes } from '@angular/router';
import { Dashboard, ExpenseClaims, Employees, Categories } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'expense-claims', component: ExpenseClaims },
  { path: 'employees', component: Employees },
  { path: 'categories', component: Categories },
];

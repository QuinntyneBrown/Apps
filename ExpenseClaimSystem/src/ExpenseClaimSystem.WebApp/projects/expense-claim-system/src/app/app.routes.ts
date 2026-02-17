import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { ExpenseClaims } from '@lib/expense-claims';
import { Employees } from '@lib/employees';
import { Categories } from '@lib/categories';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'expense-claims', component: ExpenseClaims },
  { path: 'employees', component: Employees },
  { path: 'categories', component: Categories },
];

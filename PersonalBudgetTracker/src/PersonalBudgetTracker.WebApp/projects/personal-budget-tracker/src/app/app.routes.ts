import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Budgets } from '@lib/budgets';
import { Expenses } from '@lib/expenses';
import { Incomes } from '@lib/incomes';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'budgets', component: Budgets },
  { path: 'expenses', component: Expenses },
  { path: 'incomes', component: Incomes },
];

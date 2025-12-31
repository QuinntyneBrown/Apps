import { Routes } from '@angular/router';
import { Dashboard, Budgets, Expenses, Incomes } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'budgets', component: Budgets },
  { path: 'expenses', component: Expenses },
  { path: 'incomes', component: Incomes },
];

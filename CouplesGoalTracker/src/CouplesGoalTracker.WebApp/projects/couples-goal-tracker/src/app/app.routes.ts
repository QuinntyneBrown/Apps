import { Routes } from '@angular/router';
import { Dashboard, Goals, GoalDetail } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'goals', component: Goals },
  { path: 'goals/:id', component: GoalDetail }
];

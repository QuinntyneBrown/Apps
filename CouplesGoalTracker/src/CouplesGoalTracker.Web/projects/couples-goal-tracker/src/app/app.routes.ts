import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Goals } from '@lib/goals';
import { GoalDetail } from '@lib/goal-detail';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'goals', component: Goals },
  { path: 'goals/:id', component: GoalDetail }
];

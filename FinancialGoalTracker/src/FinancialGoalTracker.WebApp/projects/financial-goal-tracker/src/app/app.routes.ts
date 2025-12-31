import { Routes } from '@angular/router';
import { Dashboard, Goals, GoalDetails, Contributions, Milestones } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'goals', component: Goals },
  { path: 'goals/:id', component: GoalDetails },
  { path: 'contributions', component: Contributions },
  { path: 'milestones', component: Milestones },
  { path: '**', redirectTo: '/dashboard' }
];

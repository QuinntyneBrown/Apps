import { Routes } from '@angular/router';


export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'goals', component: Goals },
  { path: 'goals/:id', component: GoalDetails },
  { path: 'contributions', component: Contributions },
  { path: 'milestones', component: Milestones },
  { path: '**', redirectTo: '/dashboard' }
];

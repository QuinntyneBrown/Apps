import { Routes } from '@angular/router';
import { Dashboard, Exercises, Workouts, Progress } from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'exercises', component: Exercises },
  { path: 'workouts', component: Workouts },
  { path: 'progress', component: Progress },
  { path: '**', redirectTo: '' }
];

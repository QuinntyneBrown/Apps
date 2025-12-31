import { Routes } from '@angular/router';
import { Dashboard, HabitDetail } from './pages';

export const routes: Routes = [
  {
    path: '',
    component: Dashboard,
    title: 'My Habits - Habit Tracker'
  },
  {
    path: 'habits/:id',
    component: HabitDetail,
    title: 'Habit Details - Habit Tracker'
  },
  {
    path: '**',
    redirectTo: ''
  }
];

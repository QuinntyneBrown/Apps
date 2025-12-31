import { Routes } from '@angular/router';
import { Dashboard, SleepSessions, Habits, Patterns } from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'sleep-sessions', component: SleepSessions },
  { path: 'habits', component: Habits },
  { path: 'patterns', component: Patterns },
  { path: '**', redirectTo: '' }
];

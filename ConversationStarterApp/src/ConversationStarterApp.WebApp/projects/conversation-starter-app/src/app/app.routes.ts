import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { Prompts } from './pages/prompts';
import { Favorites } from './pages/favorites';
import { Sessions } from './pages/sessions';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'prompts', component: Prompts },
  { path: 'favorites', component: Favorites },
  { path: 'sessions', component: Sessions }
];

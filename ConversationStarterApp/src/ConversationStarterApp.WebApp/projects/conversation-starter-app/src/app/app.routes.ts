import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Prompts } from '@lib/prompts';
import { Favorites } from '@lib/favorites';
import { Sessions } from '@lib/sessions';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'prompts', component: Prompts },
  { path: 'favorites', component: Favorites },
  { path: 'sessions', component: Sessions }
];

import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard';
import { Contacts } from './pages/contacts/contacts';
import { Interactions } from './pages/interactions/interactions';
import { FollowUps } from './pages/follow-ups/follow-ups';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'contacts', component: Contacts },
  { path: 'interactions', component: Interactions },
  { path: 'follow-ups', component: FollowUps }
];

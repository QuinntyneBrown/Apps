import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { Events } from './pages/events';
import { Sessions } from './pages/sessions';
import { Contacts } from './pages/contacts';
import { Notes } from './pages/notes';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'events', component: Events },
  { path: 'sessions', component: Sessions },
  { path: 'contacts', component: Contacts },
  { path: 'notes', component: Notes },
  { path: '**', redirectTo: '' }
];

import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Events } from '@lib/events';
import { Sessions } from '@lib/sessions';
import { Contacts } from '@lib/contacts';
import { Notes } from '@lib/notes';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'events', component: Events },
  { path: 'sessions', component: Sessions },
  { path: 'contacts', component: Contacts },
  { path: 'notes', component: Notes },
  { path: '**', redirectTo: '' }
];

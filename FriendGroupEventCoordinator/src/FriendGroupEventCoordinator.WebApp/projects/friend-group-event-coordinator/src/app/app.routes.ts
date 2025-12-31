import { Routes } from '@angular/router';
import { Home, GroupsList, GroupDetail, EventsList, EventDetail } from './pages';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'groups', component: GroupsList },
  { path: 'groups/:id', component: GroupDetail },
  { path: 'events', component: EventsList },
  { path: 'events/:id', component: EventDetail },
  { path: '**', redirectTo: '' }
];

import { Routes } from '@angular/router';
import { Persons, Relationships, Stories, Photos } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/persons', pathMatch: 'full' },
  { path: 'persons', component: Persons },
  { path: 'relationships', component: Relationships },
  { path: 'stories', component: Stories },
  { path: 'photos', component: Photos }
];

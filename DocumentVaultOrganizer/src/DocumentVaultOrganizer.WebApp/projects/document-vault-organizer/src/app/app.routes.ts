import { Routes } from '@angular/router';
import { Documents, Categories, Alerts } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/documents', pathMatch: 'full' },
  { path: 'documents', component: Documents },
  { path: 'categories', component: Categories },
  { path: 'alerts', component: Alerts }
];

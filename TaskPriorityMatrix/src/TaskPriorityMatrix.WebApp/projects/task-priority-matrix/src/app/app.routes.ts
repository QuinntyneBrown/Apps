import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { Tasks } from './pages/tasks';
import { Categories } from './pages/categories';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'tasks', component: Tasks },
  { path: 'categories', component: Categories },
  { path: '**', redirectTo: '' }
];

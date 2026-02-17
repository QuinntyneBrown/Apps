import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Tasks } from '@lib/tasks';
import { Categories } from '@lib/categories';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'tasks', component: Tasks },
  { path: 'categories', component: Categories },
  { path: '**', redirectTo: '' }
];

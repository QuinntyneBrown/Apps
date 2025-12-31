import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { ReadingsList } from './pages/readings-list';
import { Trends } from './pages/trends';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: Dashboard
  },
  {
    path: 'readings',
    component: ReadingsList
  },
  {
    path: 'trends',
    component: Trends
  }
];

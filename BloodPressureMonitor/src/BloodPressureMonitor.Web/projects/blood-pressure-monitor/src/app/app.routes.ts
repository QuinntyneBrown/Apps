import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { ReadingsList } from '@lib/readings-list';
import { Trends } from '@lib/trends';

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

import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { ProjectDetail } from './pages/project-detail';

export const routes: Routes = [
  {
    path: '',
    component: Dashboard
  },
  {
    path: 'projects',
    component: Dashboard
  },
  {
    path: 'projects/:id',
    component: ProjectDetail
  },
  {
    path: '**',
    redirectTo: ''
  }
];

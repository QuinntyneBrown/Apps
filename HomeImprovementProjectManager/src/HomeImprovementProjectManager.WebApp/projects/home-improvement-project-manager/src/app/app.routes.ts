import { Routes } from '@angular/router';
import { ProjectList, ProjectDetail } from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  { path: 'projects', component: ProjectList },
  { path: 'projects/:id', component: ProjectDetail }
];

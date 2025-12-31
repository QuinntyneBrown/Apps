import { Routes } from '@angular/router';
import {
  Dashboard,
  PartsList,
  PartDetail,
  ModificationsList,
  ModificationDetail,
  InstallationsList,
  InstallationDetail
} from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'parts', component: PartsList },
  { path: 'parts/new', component: PartDetail },
  { path: 'parts/:id', component: PartDetail },
  { path: 'parts/:id/edit', component: PartDetail },
  { path: 'modifications', component: ModificationsList },
  { path: 'modifications/new', component: ModificationDetail },
  { path: 'modifications/:id', component: ModificationDetail },
  { path: 'modifications/:id/edit', component: ModificationDetail },
  { path: 'installations', component: InstallationsList },
  { path: 'installations/new', component: InstallationDetail },
  { path: 'installations/:id', component: InstallationDetail },
  { path: 'installations/:id/edit', component: InstallationDetail },
  { path: '**', redirectTo: '' }
];

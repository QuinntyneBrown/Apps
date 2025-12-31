import { Routes } from '@angular/router';
import { Dashboard, AppliancesList, ApplianceDetail } from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'appliances', component: AppliancesList },
  { path: 'appliances/:id', component: ApplianceDetail },
];

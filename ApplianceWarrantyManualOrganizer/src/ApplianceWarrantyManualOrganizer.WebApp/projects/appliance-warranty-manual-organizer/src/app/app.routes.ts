import { Routes } from '@angular/router';


export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'appliances', component: AppliancesList },
  { path: 'appliances/:id', component: ApplianceDetail },
];

import { Routes } from '@angular/router';


export const routes: Routes = [
  { path: '', redirectTo: '/clients', pathMatch: 'full' },
  { path: 'clients', component: Clients },
  { path: 'projects', component: Projects },
  { path: 'invoices', component: Invoices },
  { path: 'time-entries', component: TimeEntries }
];

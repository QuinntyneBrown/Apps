import { Routes } from '@angular/router';


export const routes: Routes = [
  { path: '', redirectTo: '/persons', pathMatch: 'full' },
  { path: 'persons', component: Persons },
  { path: 'relationships', component: Relationships },
  { path: 'stories', component: Stories },
  { path: 'photos', component: Photos }
];

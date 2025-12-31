import { Routes } from '@angular/router';
import {
  Dashboard,
  UtilityBillsList,
  UtilityBillsForm,
  UsagesList,
  UsagesForm,
  SavingsTipsList,
  SavingsTipsForm
} from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'utility-bills', component: UtilityBillsList },
  { path: 'utility-bills/create', component: UtilityBillsForm },
  { path: 'utility-bills/edit/:id', component: UtilityBillsForm },
  { path: 'usages', component: UsagesList },
  { path: 'usages/create', component: UsagesForm },
  { path: 'usages/edit/:id', component: UsagesForm },
  { path: 'savings-tips', component: SavingsTipsList },
  { path: 'savings-tips/create', component: SavingsTipsForm },
  { path: 'savings-tips/edit/:id', component: SavingsTipsForm }
];

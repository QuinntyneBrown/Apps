import { Routes } from '@angular/router';
import {
  Dashboard,
  PlanList,
  PlanDetail,
  BeneficiaryList,
  ContributionList,
  ProjectionList,
  ProjectionDetail
} from './pages';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'plans', component: PlanList },
  { path: 'plans/:id', component: PlanDetail },
  { path: 'beneficiaries', component: BeneficiaryList },
  { path: 'contributions', component: ContributionList },
  { path: 'projections', component: ProjectionList },
  { path: 'projections/:id', component: ProjectionDetail }
];

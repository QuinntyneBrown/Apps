import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { PlanList } from '@lib/plan-list';
import { PlanDetail } from '@lib/plan-detail';
import { BeneficiaryList } from '@lib/beneficiary-list';
import { ContributionList } from '@lib/contribution-list';
import { ProjectionList } from '@lib/projection-list';
import { ProjectionDetail } from '@lib/projection-detail';

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

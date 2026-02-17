import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { ChoresList } from '@lib/chores-list';
import { FamilyMembersList } from '@lib/family-members-list';
import { AssignmentsList } from '@lib/assignments-list';
import { RewardsList } from '@lib/rewards-list';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'chores', component: ChoresList },
  { path: 'family-members', component: FamilyMembersList },
  { path: 'assignments', component: AssignmentsList },
  { path: 'rewards', component: RewardsList }
];

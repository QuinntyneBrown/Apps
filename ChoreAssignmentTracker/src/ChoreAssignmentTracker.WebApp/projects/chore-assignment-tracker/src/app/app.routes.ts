import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { ChoresList } from './pages/chores-list';
import { FamilyMembersList } from './pages/family-members-list';
import { AssignmentsList } from './pages/assignments-list';
import { RewardsList } from './pages/rewards-list';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'chores', component: ChoresList },
  { path: 'family-members', component: FamilyMembersList },
  { path: 'assignments', component: AssignmentsList },
  { path: 'rewards', component: RewardsList }
];

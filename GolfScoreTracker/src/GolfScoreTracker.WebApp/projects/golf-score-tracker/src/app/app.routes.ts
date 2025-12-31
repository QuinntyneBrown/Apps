import { Routes } from '@angular/router';
import {
  Home,
  CoursesList,
  CourseDetail,
  CourseForm,
  RoundsList,
  RoundDetail,
  RoundForm,
  Scorecard,
  HandicapsList,
  HandicapDetail
} from './pages';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'courses', component: CoursesList },
  { path: 'courses/new', component: CourseForm },
  { path: 'courses/:id', component: CourseDetail },
  { path: 'courses/:id/edit', component: CourseForm },
  { path: 'rounds', component: RoundsList },
  { path: 'rounds/new', component: RoundForm },
  { path: 'rounds/:id', component: RoundDetail },
  { path: 'rounds/:id/edit', component: RoundForm },
  { path: 'scorecard', component: Scorecard },
  { path: 'handicaps', component: HandicapsList },
  { path: 'handicaps/:id', component: HandicapDetail }
];

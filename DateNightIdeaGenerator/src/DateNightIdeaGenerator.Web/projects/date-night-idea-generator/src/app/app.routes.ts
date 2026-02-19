import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { DateIdeaList } from '@lib/date-idea-list';
import { DateIdeaDetail } from '@lib/date-idea-detail';
import { DateIdeaForm } from '@lib/date-idea-form';
import { ExperienceList } from '@lib/experience-list';
import { Favorites } from '@lib/favorites';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'date-ideas', component: DateIdeaList },
  { path: 'date-ideas/new', component: DateIdeaForm },
  { path: 'date-ideas/:id', component: DateIdeaDetail },
  { path: 'date-ideas/:id/edit', component: DateIdeaForm },
  { path: 'experiences', component: ExperienceList },
  { path: 'favorites', component: Favorites },
  { path: '**', redirectTo: '' }
];

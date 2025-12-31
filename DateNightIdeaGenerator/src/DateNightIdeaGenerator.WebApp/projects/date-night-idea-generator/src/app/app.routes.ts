import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { DateIdeaList } from './pages/date-idea-list';
import { DateIdeaDetail } from './pages/date-idea-detail';
import { DateIdeaForm } from './pages/date-idea-form';
import { ExperienceList } from './pages/experience-list';
import { Favorites } from './pages/favorites';

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

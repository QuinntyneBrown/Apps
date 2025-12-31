import { Routes } from '@angular/router';
import {
  Home,
  RecipesList,
  RecipeForm,
  BatchesList,
  BatchForm,
  TastingNotesList,
  TastingNoteForm
} from './pages';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'recipes', component: RecipesList },
  { path: 'recipes/new', component: RecipeForm },
  { path: 'recipes/:id', component: RecipeForm },
  { path: 'recipes/:id/edit', component: RecipeForm },
  { path: 'batches', component: BatchesList },
  { path: 'batches/new', component: BatchForm },
  { path: 'batches/:id', component: BatchForm },
  { path: 'batches/:id/edit', component: BatchForm },
  { path: 'tasting-notes', component: TastingNotesList },
  { path: 'tasting-notes/new', component: TastingNoteForm },
  { path: 'tasting-notes/:id', component: TastingNoteForm },
  { path: 'tasting-notes/:id/edit', component: TastingNoteForm },
  { path: '**', redirectTo: '' }
];

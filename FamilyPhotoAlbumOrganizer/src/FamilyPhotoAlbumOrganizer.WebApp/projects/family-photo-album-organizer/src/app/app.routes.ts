import { Routes } from '@angular/router';
import { Home } from './pages/home';
import { Albums } from './pages/albums';
import { Photos } from './pages/photos';
import { Tags } from './pages/tags';
import { People } from './pages/people';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'albums', component: Albums },
  { path: 'photos', component: Photos },
  { path: 'tags', component: Tags },
  { path: 'people', component: People },
  { path: '**', redirectTo: '' }
];

import { Routes } from '@angular/router';
import { Home } from '@lib/home';
import { Albums } from '@lib/albums';
import { Photos } from '@lib/photos';
import { Tags } from '@lib/tags';
import { People } from '@lib/people';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'albums', component: Albums },
  { path: 'photos', component: Photos },
  { path: 'tags', component: Tags },
  { path: 'people', component: People },
  { path: '**', redirectTo: '' }
];

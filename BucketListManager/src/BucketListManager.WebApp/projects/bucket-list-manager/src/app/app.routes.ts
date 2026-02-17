import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { BucketListItemDetail } from '@lib/bucket-list-item-detail';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'items/:id', component: BucketListItemDetail },
  { path: '**', redirectTo: '' }
];

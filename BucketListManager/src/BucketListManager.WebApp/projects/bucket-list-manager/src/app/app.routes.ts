import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard';
import { BucketListItemDetail } from './pages/bucket-list-item-detail/bucket-list-item-detail';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'items/:id', component: BucketListItemDetail },
  { path: '**', redirectTo: '' }
];

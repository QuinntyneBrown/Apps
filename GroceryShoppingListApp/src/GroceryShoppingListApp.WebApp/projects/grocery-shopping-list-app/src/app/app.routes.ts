import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard';
import { ListDetail } from './pages/list-detail';
import { Stores } from './pages/stores';
import { PriceHistoryPage } from './pages/price-history';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'list/:id', component: ListDetail },
  { path: 'stores', component: Stores },
  { path: 'price-history', component: PriceHistoryPage },
  { path: '**', redirectTo: '' }
];

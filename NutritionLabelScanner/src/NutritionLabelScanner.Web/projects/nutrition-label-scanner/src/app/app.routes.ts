import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'products',
    loadComponent: () => import('./pages').then(m => m.ProductList)
  },
  {
    path: 'products/:id',
    loadComponent: () => import('./pages').then(m => m.ProductForm)
  },
  {
    path: 'nutrition-infos',
    loadComponent: () => import('./pages').then(m => m.NutritionInfoList)
  },
  {
    path: 'nutrition-infos/:id',
    loadComponent: () => import('./pages').then(m => m.NutritionInfoForm)
  },
  {
    path: 'comparisons',
    loadComponent: () => import('./pages').then(m => m.ComparisonList)
  },
  {
    path: 'comparisons/:id',
    loadComponent: () => import('./pages').then(m => m.ComparisonForm)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

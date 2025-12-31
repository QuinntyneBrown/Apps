import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./pages').then(m => m.Dashboard)
  },
  {
    path: 'loans',
    loadComponent: () => import('./pages').then(m => m.LoanList)
  },
  {
    path: 'loans/new',
    loadComponent: () => import('./pages').then(m => m.LoanForm)
  },
  {
    path: 'loans/:id/edit',
    loadComponent: () => import('./pages').then(m => m.LoanForm)
  },
  {
    path: 'offers',
    loadComponent: () => import('./pages').then(m => m.OfferList)
  },
  {
    path: 'offers/new',
    loadComponent: () => import('./pages').then(m => m.OfferForm)
  },
  {
    path: 'offers/:id/edit',
    loadComponent: () => import('./pages').then(m => m.OfferForm)
  },
  {
    path: 'payment-schedules',
    loadComponent: () => import('./pages').then(m => m.PaymentScheduleList)
  },
  {
    path: 'payment-schedules/new',
    loadComponent: () => import('./pages').then(m => m.PaymentScheduleForm)
  },
  {
    path: 'payment-schedules/:id/edit',
    loadComponent: () => import('./pages').then(m => m.PaymentScheduleForm)
  }
];

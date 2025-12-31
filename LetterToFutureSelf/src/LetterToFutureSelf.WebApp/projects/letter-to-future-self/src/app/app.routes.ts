import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../pages').then(m => m.Dashboard)
  },
  {
    path: 'letters',
    loadComponent: () => import('../pages').then(m => m.Letters)
  },
  {
    path: 'letters/new',
    loadComponent: () => import('../pages').then(m => m.LetterForm)
  },
  {
    path: 'letters/:id',
    loadComponent: () => import('../pages').then(m => m.LetterForm)
  },
  {
    path: 'delivery-schedules',
    loadComponent: () => import('../pages').then(m => m.DeliverySchedules)
  },
  {
    path: 'delivery-schedules/new',
    loadComponent: () => import('../pages').then(m => m.DeliveryScheduleForm)
  },
  {
    path: 'delivery-schedules/:id',
    loadComponent: () => import('../pages').then(m => m.DeliveryScheduleForm)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

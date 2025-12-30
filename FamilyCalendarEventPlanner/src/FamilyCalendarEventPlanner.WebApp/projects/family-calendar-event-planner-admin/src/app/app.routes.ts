import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layouts/main-layout.component';

export const routes: Routes = [
    {
        path: 'login',
        loadComponent: () => import('./pages').then(m => m.Login)
    },
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            {
                path: '',
                loadComponent: () => import('./pages').then(m => m.FamilyMembers)
            },
            {
                path: 'households',
                loadComponent: () => import('./pages').then(m => m.Households)
            },
            {
                path: 'family-members',
                loadComponent: () => import('./pages').then(m => m.FamilyMembers)
            },
            {
                path: 'events',
                loadComponent: () => import('./pages').then(m => m.Events)
            },
            {
                path: 'users',
                loadComponent: () => import('./pages').then(m => m.Users)
            },
            {
                path: 'roles',
                loadComponent: () => import('./pages').then(m => m.Roles)
            }
        ]
    }
];

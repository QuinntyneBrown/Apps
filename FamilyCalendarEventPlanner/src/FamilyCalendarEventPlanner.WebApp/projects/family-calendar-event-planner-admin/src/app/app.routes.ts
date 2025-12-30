import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'login', loadComponent: () => import('./pages').then(m => m.Login)
    },
    {
        path: '', loadComponent: () => import('./pages').then(m => m.FamilyMembers)
    },
    {
        path: 'households', loadComponent: () => import('./pages').then(m => m.Households)
    },
    {
        path: 'family-members', loadComponent: () => import('./pages').then(m => m.FamilyMembers)
    },
    {
        path: 'events', loadComponent: () => import('./pages').then(m => m.Events)
    },
    {
        path: 'users', loadComponent: () => import('./pages').then(m => m.Users)
    },
    {
        path: 'roles', loadComponent: () => import('./pages').then(m => m.Roles)
    }
];

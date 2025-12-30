import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '', loadComponent: () => import('./pages').then(m => m.FamilyMembers)
    },
    {
        path: 'households', loadComponent: () => import('./pages').then(m => m.Households)
    }
];

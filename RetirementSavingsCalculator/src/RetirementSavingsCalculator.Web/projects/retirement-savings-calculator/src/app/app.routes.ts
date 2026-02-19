import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'retirement-scenarios',
    loadComponent: () => import('./pages/retirement-scenarios').then(m => m.RetirementScenarios)
  },
  {
    path: 'retirement-scenarios/:id',
    loadComponent: () => import('./pages/retirement-scenario-form').then(m => m.RetirementScenarioForm)
  },
  {
    path: 'contributions',
    loadComponent: () => import('./pages/contributions').then(m => m.Contributions)
  },
  {
    path: 'contributions/:id',
    loadComponent: () => import('./pages/contribution-form').then(m => m.ContributionForm)
  },
  {
    path: 'withdrawal-strategies',
    loadComponent: () => import('./pages/withdrawal-strategies').then(m => m.WithdrawalStrategies)
  },
  {
    path: 'withdrawal-strategies/:id',
    loadComponent: () => import('./pages/withdrawal-strategy-form').then(m => m.WithdrawalStrategyForm)
  }
];

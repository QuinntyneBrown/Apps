import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/dashboard').then(m => m.Dashboard)
  },
  {
    path: 'wallets',
    loadComponent: () => import('./pages/wallets').then(m => m.WalletsList)
  },
  {
    path: 'wallets/:id',
    loadComponent: () => import('./pages/wallets').then(m => m.WalletDetail)
  },
  {
    path: 'crypto-holdings',
    loadComponent: () => import('./pages/crypto-holdings').then(m => m.CryptoHoldingsList)
  },
  {
    path: 'crypto-holdings/:id',
    loadComponent: () => import('./pages/crypto-holdings').then(m => m.CryptoHoldingDetail)
  },
  {
    path: 'transactions',
    loadComponent: () => import('./pages/transactions').then(m => m.TransactionsList)
  },
  {
    path: 'transactions/:id',
    loadComponent: () => import('./pages/transactions').then(m => m.TransactionDetail)
  },
  {
    path: 'tax-lots',
    loadComponent: () => import('./pages/tax-lots').then(m => m.TaxLotsList)
  },
  {
    path: 'tax-lots/:id',
    loadComponent: () => import('./pages/tax-lots').then(m => m.TaxLotDetail)
  }
];

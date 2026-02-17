import { TransactionType } from './transaction-type';

export interface Transaction {
  transactionId: string;
  accountId: string;
  holdingId?: string;
  transactionDate: string;
  transactionType: TransactionType;
  symbol?: string;
  shares?: number;
  pricePerShare?: number;
  amount: number;
  fees?: number;
  notes?: string;
  totalCost: number;
}

export interface CreateTransaction {
  accountId: string;
  holdingId?: string;
  transactionDate: string;
  transactionType: TransactionType;
  symbol?: string;
  shares?: number;
  pricePerShare?: number;
  amount: number;
  fees?: number;
  notes?: string;
}

export interface UpdateTransaction {
  transactionId: string;
  accountId: string;
  holdingId?: string;
  transactionDate: string;
  transactionType: TransactionType;
  symbol?: string;
  shares?: number;
  pricePerShare?: number;
  amount: number;
  fees?: number;
  notes?: string;
}

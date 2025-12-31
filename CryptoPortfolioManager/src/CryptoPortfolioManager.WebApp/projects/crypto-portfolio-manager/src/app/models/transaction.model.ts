export enum TransactionType {
  Buy = 0,
  Sell = 1,
  Transfer = 2,
  Stake = 3,
  Reward = 4
}

export interface Transaction {
  transactionId: string;
  walletId: string;
  transactionDate: Date;
  transactionType: TransactionType;
  symbol: string;
  quantity: number;
  pricePerUnit: number;
  totalAmount: number;
  fees?: number;
  notes?: string;
  totalCost: number;
}

export interface CreateTransactionRequest {
  walletId: string;
  transactionDate: Date;
  transactionType: TransactionType;
  symbol: string;
  quantity: number;
  pricePerUnit: number;
  totalAmount: number;
  fees?: number;
  notes?: string;
}

export interface UpdateTransactionRequest {
  transactionId: string;
  walletId: string;
  transactionDate: Date;
  transactionType: TransactionType;
  symbol: string;
  quantity: number;
  pricePerUnit: number;
  totalAmount: number;
  fees?: number;
  notes?: string;
}

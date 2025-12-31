import { AccountType } from './account-type';

export interface Account {
  accountId: string;
  name: string;
  accountType: AccountType;
  institution: string;
  accountNumber?: string;
  currentBalance: number;
  isActive: boolean;
  openedDate: string;
  notes?: string;
}

export interface CreateAccount {
  name: string;
  accountType: AccountType;
  institution: string;
  accountNumber?: string;
  openedDate: string;
  notes?: string;
}

export interface UpdateAccount {
  accountId: string;
  name: string;
  accountType: AccountType;
  institution: string;
  accountNumber?: string;
  isActive: boolean;
  notes?: string;
}

import { AccountType } from './account-type.enum';

export interface DigitalAccount {
  digitalAccountId: string;
  userId: string;
  accountType: AccountType;
  accountName: string;
  username: string;
  passwordHint?: string;
  url?: string;
  desiredAction?: string;
  notes?: string;
  lastUpdatedAt: Date;
  createdAt: Date;
}

export interface CreateDigitalAccountCommand {
  userId: string;
  accountType: AccountType;
  accountName: string;
  username: string;
  passwordHint?: string;
  url?: string;
  desiredAction?: string;
  notes?: string;
}

export interface UpdateDigitalAccountCommand {
  digitalAccountId: string;
  accountType: AccountType;
  accountName: string;
  username: string;
  passwordHint?: string;
  url?: string;
  desiredAction?: string;
  notes?: string;
}

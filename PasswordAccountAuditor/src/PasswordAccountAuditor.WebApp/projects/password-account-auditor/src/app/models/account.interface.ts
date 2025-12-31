import { AccountCategory } from './account-category.enum';
import { SecurityLevel } from './security-level.enum';

export interface Account {
  accountId: string;
  userId: string;
  accountName: string;
  username: string;
  websiteUrl?: string;
  category: AccountCategory;
  securityLevel: SecurityLevel;
  hasTwoFactorAuth: boolean;
  lastPasswordChange?: string;
  lastAccessDate?: string;
  notes?: string;
  isActive: boolean;
  createdAt: string;
}

export interface CreateAccount {
  userId: string;
  accountName: string;
  username: string;
  websiteUrl?: string;
  category: AccountCategory;
  hasTwoFactorAuth: boolean;
  lastPasswordChange?: string;
  notes?: string;
}

export interface UpdateAccount {
  accountId: string;
  accountName: string;
  username: string;
  websiteUrl?: string;
  category: AccountCategory;
  hasTwoFactorAuth: boolean;
  lastPasswordChange?: string;
  notes?: string;
  isActive: boolean;
}

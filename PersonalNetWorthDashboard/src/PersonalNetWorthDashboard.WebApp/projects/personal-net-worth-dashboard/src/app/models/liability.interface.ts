import { LiabilityType } from './liability-type.enum';

export interface Liability {
  liabilityId: string;
  name: string;
  liabilityType: LiabilityType;
  currentBalance: number;
  originalAmount?: number;
  interestRate?: number;
  monthlyPayment?: number;
  creditor?: string;
  accountNumber?: string;
  dueDate?: string;
  notes?: string;
  lastUpdated: string;
  isActive: boolean;
}

export interface CreateLiability {
  name: string;
  liabilityType: LiabilityType;
  currentBalance: number;
  originalAmount?: number;
  interestRate?: number;
  monthlyPayment?: number;
  creditor?: string;
  accountNumber?: string;
  dueDate?: string;
  notes?: string;
}

export interface UpdateLiability {
  liabilityId: string;
  name: string;
  liabilityType: LiabilityType;
  currentBalance: number;
  originalAmount?: number;
  interestRate?: number;
  monthlyPayment?: number;
  creditor?: string;
  accountNumber?: string;
  dueDate?: string;
  notes?: string;
}

import { DeductionCategory } from './deduction-category';

export interface Deduction {
  deductionId: string;
  taxYearId: string;
  description: string;
  amount: number;
  date: string;
  category: DeductionCategory;
  notes?: string;
  hasReceipt: boolean;
}

export interface CreateDeduction {
  taxYearId: string;
  description: string;
  amount: number;
  date: string;
  category: DeductionCategory;
  notes?: string;
}

export interface UpdateDeduction {
  deductionId: string;
  taxYearId: string;
  description: string;
  amount: number;
  date: string;
  category: DeductionCategory;
  notes?: string;
}

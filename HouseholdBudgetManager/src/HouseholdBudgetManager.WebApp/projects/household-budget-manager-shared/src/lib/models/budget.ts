import { BudgetStatus } from './budget-status';

export interface Budget {
  budgetId: string;
  name: string;
  period: string;
  startDate: string;
  endDate: string;
  totalIncome: number;
  totalExpenses: number;
  status: BudgetStatus;
  notes?: string;
  createdAt: string;
  surplusDeficit: number;
}

export interface CreateBudget {
  name: string;
  period: string;
  startDate: string;
  endDate: string;
  notes?: string;
}

export interface UpdateBudget {
  budgetId: string;
  name: string;
  period: string;
  startDate: string;
  endDate: string;
  status: BudgetStatus;
  notes?: string;
}

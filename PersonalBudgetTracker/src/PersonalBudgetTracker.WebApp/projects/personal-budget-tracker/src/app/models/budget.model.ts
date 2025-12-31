import { BudgetStatus } from './budget-status.enum';

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

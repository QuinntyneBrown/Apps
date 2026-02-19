export interface Income {
  incomeId: string;
  budgetId: string;
  description: string;
  amount: number;
  source: string;
  incomeDate: string;
  notes?: string;
  isRecurring: boolean;
}

export interface CreateIncome {
  budgetId: string;
  description: string;
  amount: number;
  source: string;
  incomeDate: string;
  notes?: string;
  isRecurring: boolean;
}

export interface UpdateIncome {
  incomeId: string;
  budgetId: string;
  description: string;
  amount: number;
  source: string;
  incomeDate: string;
  notes?: string;
  isRecurring: boolean;
}

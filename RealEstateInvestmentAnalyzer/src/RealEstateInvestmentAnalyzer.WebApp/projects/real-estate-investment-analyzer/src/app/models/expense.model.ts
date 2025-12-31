export interface Expense {
  expenseId: string;
  propertyId: string;
  description: string;
  amount: number;
  date: string;
  category: string;
  isRecurring: boolean;
  notes?: string;
}

export interface CreateExpense {
  propertyId: string;
  description: string;
  amount: number;
  date: string;
  category: string;
  isRecurring: boolean;
  notes?: string;
}

export interface UpdateExpense {
  expenseId: string;
  propertyId: string;
  description: string;
  amount: number;
  date: string;
  category: string;
  isRecurring: boolean;
  notes?: string;
}

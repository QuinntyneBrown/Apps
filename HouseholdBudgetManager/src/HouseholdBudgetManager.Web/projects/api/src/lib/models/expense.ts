import { ExpenseCategory } from './expense-category';

export interface Expense {
  expenseId: string;
  budgetId: string;
  description: string;
  amount: number;
  category: ExpenseCategory;
  expenseDate: string;
  payee?: string;
  paymentMethod?: string;
  notes?: string;
  isRecurring: boolean;
}

export interface CreateExpense {
  budgetId: string;
  description: string;
  amount: number;
  category: ExpenseCategory;
  expenseDate: string;
  payee?: string;
  paymentMethod?: string;
  notes?: string;
  isRecurring: boolean;
}

export interface UpdateExpense {
  expenseId: string;
  budgetId: string;
  description: string;
  amount: number;
  category: ExpenseCategory;
  expenseDate: string;
  payee?: string;
  paymentMethod?: string;
  notes?: string;
  isRecurring: boolean;
}

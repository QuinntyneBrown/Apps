import { ExpenseCategory } from './expense-category.enum';

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

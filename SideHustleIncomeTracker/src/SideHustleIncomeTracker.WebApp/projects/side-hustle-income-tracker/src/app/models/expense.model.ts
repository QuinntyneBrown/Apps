export interface Expense {
  expenseId: string;
  businessId: string;
  description: string;
  amount: number;
  expenseDate: string;
  category?: string;
  vendor?: string;
  isTaxDeductible: boolean;
  notes?: string;
}

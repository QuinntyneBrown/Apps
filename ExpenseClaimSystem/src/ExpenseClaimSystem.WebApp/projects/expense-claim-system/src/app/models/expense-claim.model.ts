import { ExpenseClaimStatus } from './expense-claim-status.enum';
import { ExpenseCategoryType } from './expense-category-type.enum';

export interface ExpenseClaim {
  expenseClaimId: string;
  employeeId: string;
  employeeName?: string;
  title: string;
  description?: string;
  amount: number;
  categoryType: ExpenseCategoryType;
  submissionDate: string;
  expenseDate: string;
  status: ExpenseClaimStatus;
  receiptUrl?: string;
  approvedBy?: string;
  approvalDate?: string;
  rejectionReason?: string;
  paymentDate?: string;
}

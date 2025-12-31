import { LoanType } from './loan-type.enum';

export interface Loan {
  loanId: string;
  name: string;
  loanType: LoanType;
  requestedAmount: number;
  purpose: string;
  creditScore: number;
  notes?: string;
}

export interface CreateLoanCommand {
  name: string;
  loanType: LoanType;
  requestedAmount: number;
  purpose: string;
  creditScore: number;
  notes?: string;
}

export interface UpdateLoanCommand {
  loanId: string;
  name: string;
  loanType: LoanType;
  requestedAmount: number;
  purpose: string;
  creditScore: number;
  notes?: string;
}

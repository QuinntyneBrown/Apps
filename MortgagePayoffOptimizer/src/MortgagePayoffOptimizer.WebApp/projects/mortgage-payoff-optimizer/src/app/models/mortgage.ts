import { MortgageType } from './mortgage-type.enum';

export interface Mortgage {
  mortgageId: string;
  propertyAddress: string;
  lender: string;
  originalLoanAmount: number;
  currentBalance: number;
  interestRate: number;
  loanTermYears: number;
  monthlyPayment: number;
  startDate: string;
  mortgageType: MortgageType;
  isActive: boolean;
  notes?: string;
}

export interface CreateMortgage {
  propertyAddress: string;
  lender: string;
  originalLoanAmount: number;
  currentBalance: number;
  interestRate: number;
  loanTermYears: number;
  monthlyPayment: number;
  startDate: string;
  mortgageType: MortgageType;
  isActive: boolean;
  notes?: string;
}

export interface UpdateMortgage {
  mortgageId: string;
  propertyAddress: string;
  lender: string;
  originalLoanAmount: number;
  currentBalance: number;
  interestRate: number;
  loanTermYears: number;
  monthlyPayment: number;
  startDate: string;
  mortgageType: MortgageType;
  isActive: boolean;
  notes?: string;
}

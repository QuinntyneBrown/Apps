export interface Payment {
  paymentId: string;
  mortgageId: string;
  paymentDate: string;
  amount: number;
  principalAmount: number;
  interestAmount: number;
  extraPrincipal?: number;
  notes?: string;
}

export interface CreatePayment {
  mortgageId: string;
  paymentDate: string;
  amount: number;
  principalAmount: number;
  interestAmount: number;
  extraPrincipal?: number;
  notes?: string;
}

export interface UpdatePayment {
  paymentId: string;
  mortgageId: string;
  paymentDate: string;
  amount: number;
  principalAmount: number;
  interestAmount: number;
  extraPrincipal?: number;
  notes?: string;
}

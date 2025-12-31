export interface Offer {
  offerId: string;
  loanId: string;
  lenderName: string;
  loanAmount: number;
  interestRate: number;
  termMonths: number;
  monthlyPayment: number;
  totalCost: number;
  fees: number;
  notes?: string;
}

export interface CreateOfferCommand {
  loanId: string;
  lenderName: string;
  loanAmount: number;
  interestRate: number;
  termMonths: number;
  monthlyPayment: number;
  fees: number;
  notes?: string;
}

export interface UpdateOfferCommand {
  offerId: string;
  loanId: string;
  lenderName: string;
  loanAmount: number;
  interestRate: number;
  termMonths: number;
  monthlyPayment: number;
  fees: number;
  notes?: string;
}

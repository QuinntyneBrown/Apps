export interface Dividend {
  dividendId: string;
  holdingId: string;
  paymentDate: string;
  exDividendDate: string;
  amountPerShare: number;
  totalAmount: number;
  isReinvested: boolean;
  notes?: string;
}

export interface CreateDividend {
  holdingId: string;
  paymentDate: string;
  exDividendDate: string;
  amountPerShare: number;
  totalAmount: number;
  isReinvested: boolean;
  notes?: string;
}

export interface UpdateDividend {
  dividendId: string;
  holdingId: string;
  paymentDate: string;
  exDividendDate: string;
  amountPerShare: number;
  totalAmount: number;
  isReinvested: boolean;
  notes?: string;
}

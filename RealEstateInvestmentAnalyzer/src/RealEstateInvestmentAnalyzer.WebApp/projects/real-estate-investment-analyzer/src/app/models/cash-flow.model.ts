export interface CashFlow {
  cashFlowId: string;
  propertyId: string;
  date: string;
  income: number;
  expenses: number;
  netCashFlow: number;
  notes?: string;
}

export interface CreateCashFlow {
  propertyId: string;
  date: string;
  income: number;
  expenses: number;
  notes?: string;
}

export interface UpdateCashFlow {
  cashFlowId: string;
  propertyId: string;
  date: string;
  income: number;
  expenses: number;
  notes?: string;
}

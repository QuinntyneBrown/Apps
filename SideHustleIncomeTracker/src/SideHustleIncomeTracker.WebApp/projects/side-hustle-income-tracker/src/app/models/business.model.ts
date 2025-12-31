export interface Business {
  businessId: string;
  name: string;
  description?: string;
  startDate: string;
  isActive: boolean;
  taxId?: string;
  notes?: string;
  totalIncome: number;
  totalExpenses: number;
  netProfit: number;
}

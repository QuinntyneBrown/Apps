export interface Income {
  incomeId: string;
  businessId: string;
  description: string;
  amount: number;
  incomeDate: string;
  client?: string;
  invoiceNumber?: string;
  isPaid: boolean;
  notes?: string;
}

export interface TaxEstimate {
  taxEstimateId: string;
  businessId: string;
  taxYear: number;
  quarter: number;
  netProfit: number;
  selfEmploymentTax: number;
  incomeTax: number;
  totalEstimatedTax: number;
  isPaid: boolean;
  paymentDate?: string;
}

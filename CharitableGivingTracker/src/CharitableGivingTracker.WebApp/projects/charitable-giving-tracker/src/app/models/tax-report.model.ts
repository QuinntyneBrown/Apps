export interface TaxReport {
  taxReportId: string;
  taxYear: number;
  totalCashDonations: number;
  totalNonCashDonations: number;
  totalDeductibleAmount: number;
  generatedDate: string;
  notes?: string;
}

export interface CreateTaxReportCommand {
  taxYear: number;
  totalCashDonations: number;
  totalNonCashDonations: number;
  notes?: string;
}

export interface UpdateTaxReportCommand {
  taxReportId: string;
  taxYear: number;
  totalCashDonations: number;
  totalNonCashDonations: number;
  notes?: string;
}

export interface TaxYear {
  taxYearId: string;
  year: number;
  isFiled: boolean;
  filingDate?: string;
  totalDeductions: number;
  notes?: string;
}

export interface CreateTaxYear {
  year: number;
  notes?: string;
}

export interface UpdateTaxYear {
  taxYearId: string;
  year: number;
  isFiled: boolean;
  filingDate?: string;
  notes?: string;
}

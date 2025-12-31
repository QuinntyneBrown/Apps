export interface MarketComparison {
  marketComparisonId: string;
  userId: string;
  jobTitle: string;
  location: string;
  experienceLevel?: string;
  minSalary?: number;
  maxSalary?: number;
  medianSalary?: number;
  dataSource?: string;
  comparisonDate: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

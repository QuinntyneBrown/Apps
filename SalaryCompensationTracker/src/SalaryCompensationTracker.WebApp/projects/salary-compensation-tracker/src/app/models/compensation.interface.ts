import { CompensationType } from './compensation-type.enum';

export interface Compensation {
  compensationId: string;
  userId: string;
  compensationType: CompensationType;
  employer: string;
  jobTitle: string;
  baseSalary: number;
  currency: string;
  bonus?: number;
  stockValue?: number;
  otherCompensation?: number;
  totalCompensation: number;
  effectiveDate: string;
  endDate?: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

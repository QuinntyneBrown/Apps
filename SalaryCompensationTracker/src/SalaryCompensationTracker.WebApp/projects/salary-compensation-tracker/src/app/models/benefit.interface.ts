export interface Benefit {
  benefitId: string;
  userId: string;
  compensationId?: string;
  name: string;
  category: string;
  description?: string;
  estimatedValue?: number;
  employerContribution?: number;
  employeeContribution?: number;
  createdAt: string;
  updatedAt?: string;
}

export interface Budget {
  budgetId: string;
  projectId: string;
  category: string;
  allocatedAmount: number;
  spentAmount?: number;
  createdAt: string;
}

export interface VacationBudget {
  vacationBudgetId: string;
  tripId: string;
  category: string;
  allocatedAmount: number;
  spentAmount?: number;
  createdAt: string;
}

export interface CreateVacationBudgetCommand {
  tripId: string;
  category: string;
  allocatedAmount: number;
  spentAmount?: number;
}

export interface UpdateVacationBudgetCommand {
  vacationBudgetId: string;
  tripId: string;
  category: string;
  allocatedAmount: number;
  spentAmount?: number;
}

export enum BudgetStatus {
  Draft = 0,
  Active = 1,
  Completed = 2
}

export const BUDGET_STATUS_LABELS: Record<BudgetStatus, string> = {
  [BudgetStatus.Draft]: 'Draft',
  [BudgetStatus.Active]: 'Active',
  [BudgetStatus.Completed]: 'Completed'
};

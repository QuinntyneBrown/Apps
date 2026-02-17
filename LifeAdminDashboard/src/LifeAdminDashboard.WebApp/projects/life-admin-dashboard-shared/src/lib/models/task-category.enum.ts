export enum TaskCategory {
  Financial = 0,
  Health = 1,
  HomeMaintenance = 2,
  Vehicle = 3,
  Insurance = 4,
  Legal = 5,
  Subscriptions = 6,
  Documents = 7,
  Other = 8
}

export const TaskCategoryLabels: Record<TaskCategory, string> = {
  [TaskCategory.Financial]: 'Financial',
  [TaskCategory.Health]: 'Health',
  [TaskCategory.HomeMaintenance]: 'Home Maintenance',
  [TaskCategory.Vehicle]: 'Vehicle',
  [TaskCategory.Insurance]: 'Insurance',
  [TaskCategory.Legal]: 'Legal',
  [TaskCategory.Subscriptions]: 'Subscriptions',
  [TaskCategory.Documents]: 'Documents',
  [TaskCategory.Other]: 'Other'
};

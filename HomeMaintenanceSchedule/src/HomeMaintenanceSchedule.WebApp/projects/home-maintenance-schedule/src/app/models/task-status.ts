export enum TaskStatus {
  Scheduled = 0,
  InProgress = 1,
  Completed = 2,
  Postponed = 3,
  Cancelled = 4
}

export const TASK_STATUS_LABELS: Record<TaskStatus, string> = {
  [TaskStatus.Scheduled]: 'Scheduled',
  [TaskStatus.InProgress]: 'In Progress',
  [TaskStatus.Completed]: 'Completed',
  [TaskStatus.Postponed]: 'Postponed',
  [TaskStatus.Cancelled]: 'Cancelled'
};

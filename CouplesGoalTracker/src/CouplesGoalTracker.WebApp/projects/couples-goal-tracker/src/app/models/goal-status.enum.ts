export enum GoalStatus {
  NotStarted = 0,
  InProgress = 1,
  Completed = 2,
  OnHold = 3,
  Cancelled = 4
}

export const GoalStatusLabels: Record<GoalStatus, string> = {
  [GoalStatus.NotStarted]: 'Not Started',
  [GoalStatus.InProgress]: 'In Progress',
  [GoalStatus.Completed]: 'Completed',
  [GoalStatus.OnHold]: 'On Hold',
  [GoalStatus.Cancelled]: 'Cancelled'
};

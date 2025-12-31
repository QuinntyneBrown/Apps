export enum GoalStatus {
  NotStarted = 0,
  InProgress = 1,
  OnTrack = 2,
  AtRisk = 3,
  Completed = 4,
  Deferred = 5,
  Cancelled = 6
}

export const GOAL_STATUS_LABELS: Record<GoalStatus, string> = {
  [GoalStatus.NotStarted]: 'Not Started',
  [GoalStatus.InProgress]: 'In Progress',
  [GoalStatus.OnTrack]: 'On Track',
  [GoalStatus.AtRisk]: 'At Risk',
  [GoalStatus.Completed]: 'Completed',
  [GoalStatus.Deferred]: 'Deferred',
  [GoalStatus.Cancelled]: 'Cancelled'
};

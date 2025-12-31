export enum ActionItemStatus {
  NotStarted = 0,
  InProgress = 1,
  Completed = 2,
  Cancelled = 3,
  OnHold = 4
}

export const ACTION_ITEM_STATUS_LABELS: Record<ActionItemStatus, string> = {
  [ActionItemStatus.NotStarted]: 'Not Started',
  [ActionItemStatus.InProgress]: 'In Progress',
  [ActionItemStatus.Completed]: 'Completed',
  [ActionItemStatus.Cancelled]: 'Cancelled',
  [ActionItemStatus.OnHold]: 'On Hold'
};

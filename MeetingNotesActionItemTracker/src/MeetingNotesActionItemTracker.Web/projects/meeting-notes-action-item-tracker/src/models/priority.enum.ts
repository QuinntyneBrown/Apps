export enum Priority {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3
}

export const PRIORITY_LABELS: Record<Priority, string> = {
  [Priority.Low]: 'Low',
  [Priority.Medium]: 'Medium',
  [Priority.High]: 'High',
  [Priority.Critical]: 'Critical'
};

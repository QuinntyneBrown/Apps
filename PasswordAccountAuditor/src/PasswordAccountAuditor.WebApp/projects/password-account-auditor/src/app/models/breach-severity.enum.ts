export enum BreachSeverity {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3
}

export const BreachSeverityLabels: Record<BreachSeverity, string> = {
  [BreachSeverity.Low]: 'Low',
  [BreachSeverity.Medium]: 'Medium',
  [BreachSeverity.High]: 'High',
  [BreachSeverity.Critical]: 'Critical'
};

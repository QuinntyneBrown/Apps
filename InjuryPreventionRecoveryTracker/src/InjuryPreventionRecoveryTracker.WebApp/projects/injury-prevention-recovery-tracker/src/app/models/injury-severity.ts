export enum InjurySeverity {
  Minor = 0,
  Moderate = 1,
  Severe = 2,
  Critical = 3
}

export const INJURY_SEVERITY_LABELS: Record<InjurySeverity, string> = {
  [InjurySeverity.Minor]: 'Minor',
  [InjurySeverity.Moderate]: 'Moderate',
  [InjurySeverity.Severe]: 'Severe',
  [InjurySeverity.Critical]: 'Critical'
};

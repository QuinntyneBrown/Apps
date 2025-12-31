export enum AlertStatus {
  New = 0,
  Acknowledged = 1,
  Resolved = 2,
  Dismissed = 3
}

export const AlertStatusLabels: Record<AlertStatus, string> = {
  [AlertStatus.New]: 'New',
  [AlertStatus.Acknowledged]: 'Acknowledged',
  [AlertStatus.Resolved]: 'Resolved',
  [AlertStatus.Dismissed]: 'Dismissed'
};

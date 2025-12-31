export enum AuditStatus {
  Pending = 0,
  InProgress = 1,
  Completed = 2,
  Failed = 3
}

export const AuditStatusLabels: Record<AuditStatus, string> = {
  [AuditStatus.Pending]: 'Pending',
  [AuditStatus.InProgress]: 'In Progress',
  [AuditStatus.Completed]: 'Completed',
  [AuditStatus.Failed]: 'Failed'
};

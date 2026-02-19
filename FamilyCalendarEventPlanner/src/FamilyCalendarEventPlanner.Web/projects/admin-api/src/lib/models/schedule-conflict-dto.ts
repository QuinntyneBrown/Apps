export interface ScheduleConflictDto {
  conflictId: string;
  conflictingEventIds: string[];
  affectedMemberIds: string[];
  conflictSeverity: string;
  isResolved: boolean;
  resolvedAt?: string;
}

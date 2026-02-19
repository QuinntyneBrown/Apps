import { ConflictSeverity } from './enums';

export interface ScheduleConflict {
  conflictId: string;
  conflictingEventIds: string[];
  affectedMemberIds: string[];
  conflictSeverity: ConflictSeverity;
  isResolved: boolean;
  resolvedAt: string | null;
}

export interface CreateConflictRequest {
  conflictingEventIds: string[];
  affectedMemberIds: string[];
  conflictSeverity: ConflictSeverity;
}

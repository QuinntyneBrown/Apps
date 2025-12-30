export interface CreateConflictCommand {
  conflictingEventIds: string[];
  affectedMemberIds: string[];
  conflictSeverity: string;
}

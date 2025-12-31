export interface Distraction {
  distractionId: string;
  focusSessionId: string;
  type: string;
  description?: string | null;
  occurredAt: string;
  durationMinutes?: number | null;
  isInternal: boolean;
  createdAt: string;
}

export interface CreateDistractionCommand {
  focusSessionId: string;
  type: string;
  description?: string;
  occurredAt: string;
  durationMinutes?: number;
  isInternal: boolean;
}

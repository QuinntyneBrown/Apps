import { SessionType } from './session-type.enum';

export interface FocusSession {
  focusSessionId: string;
  userId: string;
  sessionType: SessionType;
  name: string;
  plannedDurationMinutes: number;
  startTime: string;
  endTime?: string | null;
  notes?: string | null;
  focusScore?: number | null;
  isCompleted: boolean;
  actualDurationMinutes?: number | null;
  distractionCount: number;
  createdAt: string;
}

export interface CreateFocusSessionCommand {
  userId: string;
  sessionType: SessionType;
  name: string;
  plannedDurationMinutes: number;
  startTime: string;
  notes?: string;
}

export interface UpdateFocusSessionCommand {
  focusSessionId?: string;
  name?: string;
  notes?: string;
  focusScore?: number;
}

export interface CompleteFocusSessionCommand {
  focusSessionId?: string;
  focusScore?: number;
  notes?: string;
}

export interface PlaySession {
  playSessionId: string;
  userId: string;
  gameId: string;
  startTime: string;
  endTime?: string;
  durationMinutes?: number;
  notes?: string;
  createdAt: string;
}

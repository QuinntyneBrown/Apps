import { SessionType } from './session-type.enum';

export interface Session {
  sessionId: string;
  userId: string;
  title: string;
  sessionType: SessionType;
  sessionDate: string;
  location?: string;
  client?: string;
  notes?: string;
  createdAt: string;
  photoCount: number;
}

export interface CreateSession {
  userId: string;
  title: string;
  sessionType: SessionType;
  sessionDate: string;
  location?: string;
  client?: string;
  notes?: string;
}

export interface UpdateSession {
  sessionId: string;
  title: string;
  sessionType: SessionType;
  sessionDate: string;
  location?: string;
  client?: string;
  notes?: string;
}

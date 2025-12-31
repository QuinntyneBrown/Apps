export interface Session {
  sessionId: string;
  userId: string;
  eventId: string;
  title: string;
  speaker?: string;
  description?: string;
  startTime: string;
  endTime?: string;
  room?: string;
  plansToAttend: boolean;
  didAttend: boolean;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateSessionCommand {
  eventId: string;
  title: string;
  speaker?: string;
  description?: string;
  startTime: string;
  endTime?: string;
  room?: string;
  plansToAttend: boolean;
  didAttend: boolean;
  notes?: string;
}

export interface UpdateSessionCommand {
  sessionId: string;
  eventId: string;
  title: string;
  speaker?: string;
  description?: string;
  startTime: string;
  endTime?: string;
  room?: string;
  plansToAttend: boolean;
  didAttend: boolean;
  notes?: string;
}

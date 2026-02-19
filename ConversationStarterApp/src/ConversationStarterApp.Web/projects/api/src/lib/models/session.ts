export interface Session {
  sessionId: string;
  userId: string;
  title: string;
  startTime: Date;
  endTime?: Date;
  participants?: string;
  promptsUsed?: string;
  notes?: string;
  wasSuccessful: boolean;
  createdAt: Date;
  updatedAt?: Date;
  duration?: string;
}

export interface CreateSessionRequest {
  userId: string;
  title: string;
  startTime?: Date;
  participants?: string;
  promptsUsed?: string;
}

export interface UpdateSessionRequest {
  sessionId: string;
  title: string;
  participants?: string;
  promptsUsed?: string;
  notes?: string;
  wasSuccessful: boolean;
}

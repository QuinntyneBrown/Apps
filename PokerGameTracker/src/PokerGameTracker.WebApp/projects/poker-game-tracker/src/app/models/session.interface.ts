import { GameType } from './game-type.enum';

export interface Session {
  sessionId: string;
  userId: string;
  gameType: GameType;
  startTime: string;
  endTime?: string;
  buyIn: number;
  cashOut?: number;
  location?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateSession {
  userId: string;
  gameType: GameType;
  startTime: string;
  buyIn: number;
  location?: string;
  notes?: string;
}

export interface UpdateSession {
  sessionId: string;
  userId: string;
  gameType: GameType;
  startTime: string;
  endTime?: string;
  buyIn: number;
  cashOut?: number;
  location?: string;
  notes?: string;
}

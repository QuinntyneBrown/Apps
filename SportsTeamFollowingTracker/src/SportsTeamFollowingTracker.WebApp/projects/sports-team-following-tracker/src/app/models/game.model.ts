export interface Game {
  gameId: string;
  userId: string;
  teamId: string;
  teamName?: string;
  gameDate: Date;
  opponent: string;
  teamScore?: number;
  opponentScore?: number;
  isWin?: boolean;
  notes?: string;
  createdAt: Date;
  isCompleted: boolean;
}

export interface CreateGameRequest {
  teamId: string;
  gameDate: Date;
  opponent: string;
  teamScore?: number;
  opponentScore?: number;
  notes?: string;
}

export interface UpdateGameRequest {
  gameId: string;
  teamId: string;
  gameDate: Date;
  opponent: string;
  teamScore?: number;
  opponentScore?: number;
  notes?: string;
}

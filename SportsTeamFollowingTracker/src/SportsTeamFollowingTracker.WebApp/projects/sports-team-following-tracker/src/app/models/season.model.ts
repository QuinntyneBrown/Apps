export interface Season {
  seasonId: string;
  userId: string;
  teamId: string;
  seasonName: string;
  year: number;
  wins: number;
  losses: number;
  notes?: string;
  createdAt: Date;
  totalGames: number;
  winPercentage: number;
}

export interface CreateSeasonRequest {
  teamId: string;
  seasonName: string;
  year: number;
  wins: number;
  losses: number;
  notes?: string;
}

export interface UpdateSeasonRequest {
  seasonId: string;
  teamId: string;
  seasonName: string;
  year: number;
  wins: number;
  losses: number;
  notes?: string;
}

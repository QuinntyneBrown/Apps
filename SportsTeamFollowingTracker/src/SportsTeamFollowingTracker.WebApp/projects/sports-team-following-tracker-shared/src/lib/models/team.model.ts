import { Sport } from './sport.enum';

export interface Team {
  teamId: string;
  userId: string;
  name: string;
  sport: Sport;
  league?: string;
  city?: string;
  isFavorite: boolean;
  createdAt: Date;
  gamesCount: number;
}

export interface CreateTeamRequest {
  name: string;
  sport: Sport;
  league?: string;
  city?: string;
  isFavorite: boolean;
}

export interface UpdateTeamRequest {
  teamId: string;
  name: string;
  sport: Sport;
  league?: string;
  city?: string;
  isFavorite: boolean;
}

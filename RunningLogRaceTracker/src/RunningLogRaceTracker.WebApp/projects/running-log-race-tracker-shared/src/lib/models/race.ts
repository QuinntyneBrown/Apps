import { RaceType } from './race-type';

export interface Race {
  raceId: string;
  userId: string;
  name: string;
  raceType: RaceType;
  raceDate: string;
  location: string;
  distance: number;
  finishTimeMinutes: number | null;
  goalTimeMinutes: number | null;
  placement: number | null;
  isCompleted: boolean;
  notes: string | null;
  createdAt: string;
}

export interface CreateRaceRequest {
  userId: string;
  name: string;
  raceType: RaceType;
  raceDate: string;
  location: string;
  distance: number;
  finishTimeMinutes?: number;
  goalTimeMinutes?: number;
  placement?: number;
  isCompleted: boolean;
  notes?: string;
}

export interface UpdateRaceRequest {
  name: string;
  raceType: RaceType;
  raceDate: string;
  location: string;
  distance: number;
  finishTimeMinutes?: number;
  goalTimeMinutes?: number;
  placement?: number;
  isCompleted: boolean;
  notes?: string;
}

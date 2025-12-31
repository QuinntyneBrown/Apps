import { FishSpecies } from './fish-species.enum';

export interface Catch {
  catchId: string;
  userId: string;
  tripId: string;
  species: FishSpecies;
  length?: number;
  weight?: number;
  catchTime: string;
  baitUsed?: string;
  wasReleased: boolean;
  notes?: string;
  photoUrl?: string;
  createdAt: string;
}

export interface CreateCatchRequest {
  userId: string;
  tripId: string;
  species: FishSpecies;
  length?: number;
  weight?: number;
  catchTime: string;
  baitUsed?: string;
  wasReleased: boolean;
  notes?: string;
  photoUrl?: string;
}

export interface UpdateCatchRequest extends CreateCatchRequest {
  catchId: string;
}

import { LocationType } from './location-type.enum';

export interface Spot {
  spotId: string;
  userId: string;
  name: string;
  locationType: LocationType;
  latitude?: number;
  longitude?: number;
  description?: string;
  waterBodyName?: string;
  directions?: string;
  isFavorite: boolean;
  createdAt: string;
  tripCount: number;
}

export interface CreateSpotRequest {
  userId: string;
  name: string;
  locationType: LocationType;
  latitude?: number;
  longitude?: number;
  description?: string;
  waterBodyName?: string;
  directions?: string;
  isFavorite: boolean;
}

export interface UpdateSpotRequest extends CreateSpotRequest {
  spotId: string;
}

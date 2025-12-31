import { CampsiteType } from './campsite-type.enum';

export interface Campsite {
  campsiteId: string;
  userId: string;
  name: string;
  location: string;
  campsiteType: CampsiteType;
  description?: string;
  hasElectricity: boolean;
  hasWater: boolean;
  costPerNight?: number;
  isFavorite: boolean;
  createdAt: Date;
}

export interface CreateCampsite {
  userId: string;
  name: string;
  location: string;
  campsiteType: CampsiteType;
  description?: string;
  hasElectricity: boolean;
  hasWater: boolean;
  costPerNight?: number;
  isFavorite: boolean;
}

export interface UpdateCampsite {
  campsiteId: string;
  name: string;
  location: string;
  campsiteType: CampsiteType;
  description?: string;
  hasElectricity: boolean;
  hasWater: boolean;
  costPerNight?: number;
  isFavorite: boolean;
}

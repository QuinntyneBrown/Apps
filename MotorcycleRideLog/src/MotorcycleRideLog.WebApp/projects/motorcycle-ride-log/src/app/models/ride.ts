import { RideType } from './ride-type';
import { WeatherCondition } from './weather-condition';

export interface Ride {
  rideId: string;
  userId: string;
  motorcycleId: string;
  routeId?: string;
  rideDate: string;
  distanceMiles: number;
  durationMinutes?: number;
  type: RideType;
  startLocation?: string;
  endLocation?: string;
  weather?: WeatherCondition;
  averageSpeed?: number;
  fuelUsed?: number;
  notes?: string;
  rating?: number;
  createdAt: string;
}

export interface CreateRide {
  userId: string;
  motorcycleId: string;
  routeId?: string;
  rideDate: string;
  distanceMiles: number;
  durationMinutes?: number;
  type: RideType;
  startLocation?: string;
  endLocation?: string;
  weather?: WeatherCondition;
  averageSpeed?: number;
  fuelUsed?: number;
  notes?: string;
  rating?: number;
}

export interface UpdateRide {
  rideId: string;
  motorcycleId: string;
  routeId?: string;
  rideDate: string;
  distanceMiles: number;
  durationMinutes?: number;
  type: RideType;
  startLocation?: string;
  endLocation?: string;
  weather?: WeatherCondition;
  averageSpeed?: number;
  fuelUsed?: number;
  notes?: string;
  rating?: number;
}

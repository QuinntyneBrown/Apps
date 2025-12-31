export interface Route {
  routeId: string;
  userId: string;
  name: string;
  description?: string;
  startLocation?: string;
  endLocation?: string;
  distanceMiles?: number;
  waypoints?: string;
  estimatedDurationMinutes?: number;
  difficulty?: string;
  isFavorite: boolean;
  notes?: string;
  createdAt: string;
}

export interface CreateRoute {
  userId: string;
  name: string;
  description?: string;
  startLocation?: string;
  endLocation?: string;
  distanceMiles?: number;
  waypoints?: string;
  estimatedDurationMinutes?: number;
  difficulty?: string;
  isFavorite: boolean;
  notes?: string;
}

export interface UpdateRoute {
  routeId: string;
  name: string;
  description?: string;
  startLocation?: string;
  endLocation?: string;
  distanceMiles?: number;
  waypoints?: string;
  estimatedDurationMinutes?: number;
  difficulty?: string;
  isFavorite: boolean;
  notes?: string;
}

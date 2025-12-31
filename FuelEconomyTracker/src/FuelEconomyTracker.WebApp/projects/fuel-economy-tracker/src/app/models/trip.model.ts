export interface Trip {
  tripId: string;
  vehicleId: string;
  startDate: Date;
  endDate?: Date;
  startOdometer: number;
  endOdometer?: number;
  distance?: number;
  purpose?: string;
  startLocation?: string;
  endLocation?: string;
  averageMPG?: number;
  notes?: string;
}

export interface CreateTripRequest {
  vehicleId: string;
  startDate: Date;
  startOdometer: number;
  purpose?: string;
  startLocation?: string;
  notes?: string;
}

export interface UpdateTripRequest {
  startDate: Date;
  endDate?: Date;
  startOdometer: number;
  endOdometer?: number;
  purpose?: string;
  startLocation?: string;
  endLocation?: string;
  averageMPG?: number;
  notes?: string;
}

export interface CompleteTripRequest {
  endDate: Date;
  endOdometer: number;
  endLocation?: string;
}

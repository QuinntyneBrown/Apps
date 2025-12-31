export interface Trip {
  tripId: string;
  userId: string;
  spotId?: string;
  spotName?: string;
  tripDate: string;
  startTime: string;
  endTime?: string;
  weatherConditions?: string;
  waterTemperature?: number;
  airTemperature?: number;
  notes?: string;
  createdAt: string;
  catchCount: number;
  durationInHours?: number;
}

export interface CreateTripRequest {
  userId: string;
  spotId?: string;
  tripDate: string;
  startTime: string;
  endTime?: string;
  weatherConditions?: string;
  waterTemperature?: number;
  airTemperature?: number;
  notes?: string;
}

export interface UpdateTripRequest extends CreateTripRequest {
  tripId: string;
}

export interface Trip {
  tripId: string;
  userId: string;
  name: string;
  destination?: string;
  startDate?: string;
  endDate?: string;
  createdAt: string;
}

export interface CreateTripCommand {
  userId: string;
  name: string;
  destination?: string;
  startDate?: string;
  endDate?: string;
}

export interface UpdateTripCommand {
  tripId: string;
  name: string;
  destination?: string;
  startDate?: string;
  endDate?: string;
}

export interface Trip {
  tripId: string;
  userId: string;
  name: string;
  campsiteId?: string;
  startDate: Date;
  endDate: Date;
  numberOfPeople: number;
  notes?: string;
  createdAt: Date;
}

export interface CreateTrip {
  userId: string;
  name: string;
  campsiteId?: string;
  startDate: Date;
  endDate: Date;
  numberOfPeople: number;
  notes?: string;
}

export interface UpdateTrip {
  tripId: string;
  name: string;
  campsiteId?: string;
  startDate: Date;
  endDate: Date;
  numberOfPeople: number;
  notes?: string;
}

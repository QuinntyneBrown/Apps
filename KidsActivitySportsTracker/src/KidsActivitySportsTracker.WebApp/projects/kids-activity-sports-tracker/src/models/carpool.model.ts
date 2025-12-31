export interface Carpool {
  carpoolId: string;
  userId: string;
  name: string;
  driverName?: string;
  driverContact?: string;
  pickupTime?: string;
  pickupLocation?: string;
  dropoffTime?: string;
  dropoffLocation?: string;
  participants?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateCarpool {
  userId: string;
  name: string;
  driverName?: string;
  driverContact?: string;
  pickupTime?: string;
  pickupLocation?: string;
  dropoffTime?: string;
  dropoffLocation?: string;
  participants?: string;
  notes?: string;
}

export interface UpdateCarpool {
  carpoolId: string;
  name: string;
  driverName?: string;
  driverContact?: string;
  pickupTime?: string;
  pickupLocation?: string;
  dropoffTime?: string;
  dropoffLocation?: string;
  participants?: string;
  notes?: string;
}

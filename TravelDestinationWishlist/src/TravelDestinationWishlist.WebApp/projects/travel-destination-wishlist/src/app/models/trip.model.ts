export interface Trip {
  tripId: string;
  userId: string;
  destinationId: string;
  startDate: string;
  endDate: string;
  totalCost?: number;
  accommodation?: string;
  transportation?: string;
  notes?: string;
  createdAt: string;
}

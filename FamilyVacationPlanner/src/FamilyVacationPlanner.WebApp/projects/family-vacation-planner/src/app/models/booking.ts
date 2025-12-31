export interface Booking {
  bookingId: string;
  tripId: string;
  type: string;
  confirmationNumber?: string;
  cost?: number;
  createdAt: string;
}

export interface CreateBookingCommand {
  tripId: string;
  type: string;
  confirmationNumber?: string;
  cost?: number;
}

export interface UpdateBookingCommand {
  bookingId: string;
  tripId: string;
  type: string;
  confirmationNumber?: string;
  cost?: number;
}

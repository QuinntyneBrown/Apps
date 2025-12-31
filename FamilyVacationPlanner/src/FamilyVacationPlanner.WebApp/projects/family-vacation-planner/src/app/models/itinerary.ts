export interface Itinerary {
  itineraryId: string;
  tripId: string;
  date: string;
  activity?: string;
  location?: string;
  createdAt: string;
}

export interface CreateItineraryCommand {
  tripId: string;
  date: string;
  activity?: string;
  location?: string;
}

export interface UpdateItineraryCommand {
  itineraryId: string;
  tripId: string;
  date: string;
  activity?: string;
  location?: string;
}

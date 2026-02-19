export interface Event {
  eventId: string;
  createdByNeighborId: string;
  title: string;
  description: string;
  eventDateTime: string;
  location?: string;
  isPublic: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateEvent {
  createdByNeighborId: string;
  title: string;
  description: string;
  eventDateTime: string;
  location?: string;
  isPublic: boolean;
}

export interface UpdateEvent {
  eventId: string;
  title: string;
  description: string;
  eventDateTime: string;
  location?: string;
  isPublic: boolean;
}

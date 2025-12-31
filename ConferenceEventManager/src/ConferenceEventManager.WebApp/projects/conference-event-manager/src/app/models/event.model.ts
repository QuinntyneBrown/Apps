import { EventType } from './event-type.enum';

export interface Event {
  eventId: string;
  userId: string;
  name: string;
  eventType: EventType;
  startDate: string;
  endDate: string;
  location?: string;
  isVirtual: boolean;
  website?: string;
  registrationFee?: number;
  isRegistered: boolean;
  didAttend: boolean;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateEventCommand {
  name: string;
  eventType: EventType;
  startDate: string;
  endDate: string;
  location?: string;
  isVirtual: boolean;
  website?: string;
  registrationFee?: number;
  isRegistered: boolean;
  didAttend: boolean;
  notes?: string;
}

export interface UpdateEventCommand {
  eventId: string;
  name: string;
  eventType: EventType;
  startDate: string;
  endDate: string;
  location?: string;
  isVirtual: boolean;
  website?: string;
  registrationFee?: number;
  isRegistered: boolean;
  didAttend: boolean;
  notes?: string;
}

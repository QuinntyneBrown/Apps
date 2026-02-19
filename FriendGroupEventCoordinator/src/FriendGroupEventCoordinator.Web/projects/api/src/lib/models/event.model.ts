import { EventType } from './event-type.enum';

export interface Event {
  eventId: string;
  groupId: string;
  createdByUserId: string;
  title: string;
  description: string;
  eventType: EventType;
  startDateTime: string;
  endDateTime?: string;
  location?: string;
  maxAttendees?: number;
  isCancelled: boolean;
  createdAt: string;
  updatedAt?: string;
  confirmedAttendeeCount: number;
}

export interface CreateEvent {
  groupId: string;
  createdByUserId: string;
  title: string;
  description: string;
  eventType: EventType;
  startDateTime: string;
  endDateTime?: string;
  location?: string;
  maxAttendees?: number;
}

export interface UpdateEvent {
  title?: string;
  description?: string;
  eventType?: EventType;
  startDateTime?: string;
  endDateTime?: string;
  location?: string;
  maxAttendees?: number;
}

import { EventType, EventStatus, RecurrenceFrequency } from './enums';

export interface RecurrencePattern {
  frequency: RecurrenceFrequency;
  interval: number;
  endDate: string | null;
  daysOfWeek: string[];
}

export interface CalendarEvent {
  eventId: string;
  familyId: string;
  creatorId: string;
  title: string;
  description: string;
  startTime: string;
  endTime: string;
  location: string;
  eventType: EventType;
  recurrencePattern: RecurrencePattern;
  status: EventStatus;
}

export interface CreateEventRequest {
  familyId: string;
  creatorId: string;
  title: string;
  description?: string;
  startTime: string;
  endTime: string;
  location?: string;
  eventType: EventType;
  recurrencePattern?: RecurrencePattern;
}

export interface UpdateEventRequest {
  eventId: string;
  title?: string;
  description?: string;
  startTime?: string;
  endTime?: string;
  location?: string;
  eventType?: EventType;
  recurrencePattern?: RecurrencePattern;
}

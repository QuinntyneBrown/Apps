export type EventType = 'Appointment' | 'FamilyDinner' | 'Sports' | 'School' | 'Vacation' | 'Birthday' | 'Other';

export type EventStatus = 'Scheduled' | 'Cancelled' | 'Completed';

export type RecurrenceFrequency = 'None' | 'Daily' | 'Weekly' | 'Monthly' | 'Yearly';

export interface CalendarEventDto {
  eventId: string;
  familyId: string;
  creatorId: string;
  title: string;
  description: string;
  startTime: string;
  endTime: string;
  location: string;
  eventType: EventType;
  recurrencePattern: RecurrencePatternDto;
  status: EventStatus;
}

export interface RecurrencePatternDto {
  frequency: RecurrenceFrequency;
  interval: number;
  endDate?: string;
  daysOfWeek: string[];
}

export interface CreateEventCommand {
  familyId: string;
  creatorId: string;
  title: string;
  description?: string;
  startTime: string;
  endTime: string;
  location?: string;
  eventType: EventType;
  recurrencePattern?: RecurrencePatternDto;
}

export interface UpdateEventCommand {
  eventId: string;
  title?: string;
  description?: string;
  startTime?: string;
  endTime?: string;
  location?: string;
  eventType?: EventType;
  recurrencePattern?: RecurrencePatternDto;
}

export const EVENT_TYPES: { value: EventType; label: string }[] = [
  { value: 'Appointment', label: 'Appointment' },
  { value: 'FamilyDinner', label: 'Family Dinner' },
  { value: 'Sports', label: 'Sports' },
  { value: 'School', label: 'School' },
  { value: 'Vacation', label: 'Vacation' },
  { value: 'Birthday', label: 'Birthday' },
  { value: 'Other', label: 'Other' }
];

export const EVENT_STATUSES: { value: EventStatus; label: string }[] = [
  { value: 'Scheduled', label: 'Scheduled' },
  { value: 'Cancelled', label: 'Cancelled' },
  { value: 'Completed', label: 'Completed' }
];

export function getEventTypeLabel(eventType: EventType): string {
  return EVENT_TYPES.find(t => t.value === eventType)?.label || eventType;
}

export function getEventStatusLabel(status: EventStatus): string {
  return EVENT_STATUSES.find(s => s.value === status)?.label || status;
}

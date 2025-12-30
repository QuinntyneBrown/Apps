export interface CalendarEventDto {
  eventId: string;
  familyId: string;
  creatorId: string;
  title: string;
  description: string;
  startTime: string;
  endTime: string;
  location: string;
  eventType: string;
  recurrencePattern: RecurrencePatternDto;
  status: string;
}

export interface RecurrencePatternDto {
  frequency: string;
  interval: number;
  endDate?: string;
  daysOfWeek: string[];
}

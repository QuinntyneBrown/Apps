export interface EventAttendeeDto {
  attendeeId: string;
  eventId: string;
  familyMemberId: string;
  rsvpStatus: string;
  responseTime?: string;
  notes: string;
}

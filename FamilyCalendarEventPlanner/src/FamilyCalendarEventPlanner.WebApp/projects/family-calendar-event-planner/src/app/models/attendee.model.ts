import { RSVPStatus } from './enums';

export interface EventAttendee {
  attendeeId: string;
  eventId: string;
  familyMemberId: string;
  rsvpStatus: RSVPStatus;
  responseTime: string | null;
  notes: string;
}

export interface AddAttendeeRequest {
  eventId: string;
  familyMemberId: string;
  notes?: string;
}

export interface RespondToEventRequest {
  attendeeId: string;
  rsvpStatus: RSVPStatus;
  notes?: string;
}

import { RSVPResponse } from './rsvp-response.enum';

export interface RSVP {
  rsvpId: string;
  eventId: string;
  memberId: string;
  userId: string;
  response: RSVPResponse;
  additionalGuests: number;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateRSVP {
  eventId: string;
  memberId: string;
  userId: string;
  response: RSVPResponse;
  additionalGuests: number;
  notes?: string;
}

export interface UpdateRSVP {
  response?: RSVPResponse;
  additionalGuests?: number;
  notes?: string;
}

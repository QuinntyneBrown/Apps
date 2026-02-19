export interface Meeting {
  meetingId: string;
  userId: string;
  title: string;
  meetingDateTime: string;
  durationMinutes?: number;
  location?: string;
  attendees: string[];
  agenda?: string;
  summary?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateMeetingDto {
  userId: string;
  title: string;
  meetingDateTime: string;
  durationMinutes?: number;
  location?: string;
  attendees?: string[];
  agenda?: string;
  summary?: string;
}

export interface UpdateMeetingDto {
  meetingId: string;
  title: string;
  meetingDateTime: string;
  durationMinutes?: number;
  location?: string;
  attendees?: string[];
  agenda?: string;
  summary?: string;
}

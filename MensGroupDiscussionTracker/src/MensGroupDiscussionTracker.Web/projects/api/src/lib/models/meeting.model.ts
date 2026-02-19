export interface Meeting {
  meetingId: string;
  groupId: string;
  title: string;
  meetingDateTime: string;
  location?: string;
  notes?: string;
  attendeeCount: number;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateMeeting {
  groupId: string;
  title: string;
  meetingDateTime: string;
  location?: string;
  notes?: string;
  attendeeCount: number;
}

export interface UpdateMeeting {
  meetingId: string;
  title: string;
  meetingDateTime: string;
  location?: string;
  notes?: string;
  attendeeCount: number;
}

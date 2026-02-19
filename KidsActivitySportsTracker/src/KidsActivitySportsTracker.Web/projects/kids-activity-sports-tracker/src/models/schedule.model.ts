export interface Schedule {
  scheduleId: string;
  activityId: string;
  eventType: string;
  dateTime: string;
  location?: string;
  durationMinutes?: number;
  notes?: string;
  isConfirmed: boolean;
  createdAt: string;
}

export interface CreateSchedule {
  activityId: string;
  eventType: string;
  dateTime: string;
  location?: string;
  durationMinutes?: number;
  notes?: string;
  isConfirmed: boolean;
}

export interface UpdateSchedule {
  scheduleId: string;
  eventType: string;
  dateTime: string;
  location?: string;
  durationMinutes?: number;
  notes?: string;
  isConfirmed: boolean;
}

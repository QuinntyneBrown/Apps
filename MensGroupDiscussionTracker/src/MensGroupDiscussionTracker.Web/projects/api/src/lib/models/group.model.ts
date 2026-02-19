export interface Group {
  groupId: string;
  createdByUserId: string;
  name: string;
  description?: string;
  meetingSchedule?: string;
  isActive: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateGroup {
  createdByUserId: string;
  name: string;
  description?: string;
  meetingSchedule?: string;
}

export interface UpdateGroup {
  groupId: string;
  name: string;
  description?: string;
  meetingSchedule?: string;
  isActive: boolean;
}

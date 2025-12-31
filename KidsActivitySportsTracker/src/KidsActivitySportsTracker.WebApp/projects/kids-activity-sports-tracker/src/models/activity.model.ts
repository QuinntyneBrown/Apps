import { ActivityType } from './activity-type.enum';

export interface Activity {
  activityId: string;
  userId: string;
  childName: string;
  name: string;
  activityType: ActivityType;
  organization?: string;
  coachName?: string;
  coachContact?: string;
  season?: string;
  startDate?: string;
  endDate?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateActivity {
  userId: string;
  childName: string;
  name: string;
  activityType: ActivityType;
  organization?: string;
  coachName?: string;
  coachContact?: string;
  season?: string;
  startDate?: string;
  endDate?: string;
  notes?: string;
}

export interface UpdateActivity {
  activityId: string;
  childName: string;
  name: string;
  activityType: ActivityType;
  organization?: string;
  coachName?: string;
  coachContact?: string;
  season?: string;
  startDate?: string;
  endDate?: string;
  notes?: string;
}

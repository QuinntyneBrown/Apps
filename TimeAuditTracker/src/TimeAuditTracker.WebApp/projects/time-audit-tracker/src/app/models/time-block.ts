import { ActivityCategory } from './activity-category';

export interface TimeBlock {
  timeBlockId: string;
  userId: string;
  category: ActivityCategory;
  description: string;
  startTime: Date;
  endTime?: Date;
  notes?: string;
  tags?: string;
  isProductive: boolean;
  createdAt: Date;
  durationInMinutes?: number;
}

export interface CreateTimeBlockRequest {
  userId: string;
  category: ActivityCategory;
  description: string;
  startTime: Date;
  endTime?: Date;
  notes?: string;
  tags?: string;
  isProductive: boolean;
}

export interface UpdateTimeBlockRequest {
  category: ActivityCategory;
  description: string;
  startTime: Date;
  endTime?: Date;
  notes?: string;
  tags?: string;
  isProductive: boolean;
}

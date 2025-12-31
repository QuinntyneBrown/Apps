import { PriorityLevel } from './priority-level.enum';

export interface WeeklyPriority {
  weeklyPriorityId: string;
  weeklyReviewId: string;
  title: string;
  description?: string;
  level: PriorityLevel;
  category?: string;
  targetDate?: string;
  isCompleted: boolean;
  createdAt: string;
}

import { GoalCategory } from './goal-category.enum';
import { GoalStatus } from './goal-status.enum';

export interface Goal {
  goalId: string;
  userId: string;
  title: string;
  description: string;
  category: GoalCategory;
  status: GoalStatus;
  targetDate: string | null;
  completedDate: string | null;
  priority: number;
  isShared: boolean;
  createdAt: string;
  updatedAt: string | null;
  completionPercentage: number;
}

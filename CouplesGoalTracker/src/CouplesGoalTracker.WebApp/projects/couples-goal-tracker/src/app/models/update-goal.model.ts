import { GoalCategory } from './goal-category.enum';
import { GoalStatus } from './goal-status.enum';

export interface UpdateGoal {
  goalId: string;
  title: string;
  description: string;
  category: GoalCategory;
  status: GoalStatus;
  targetDate: string | null;
  priority: number;
  isShared: boolean;
}

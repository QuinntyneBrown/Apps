import { GoalCategory } from './goal-category.enum';

export interface CreateGoal {
  userId: string;
  title: string;
  description: string;
  category: GoalCategory;
  targetDate: string | null;
  priority: number;
  isShared: boolean;
}

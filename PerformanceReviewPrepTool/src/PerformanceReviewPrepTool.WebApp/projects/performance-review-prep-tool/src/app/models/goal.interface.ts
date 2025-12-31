import { GoalStatus } from './goal-status.enum';

export interface Goal {
  goalId: string;
  userId: string;
  reviewPeriodId: string;
  title: string;
  description: string;
  status: GoalStatus;
  targetDate?: string;
  completedDate?: string;
  progressPercentage: number;
  successMetrics?: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateGoal {
  userId: string;
  reviewPeriodId: string;
  title: string;
  description: string;
  status: GoalStatus;
  targetDate?: string;
  progressPercentage: number;
  successMetrics?: string;
  notes?: string;
}

export interface UpdateGoal {
  goalId: string;
  title: string;
  description: string;
  status: GoalStatus;
  targetDate?: string;
  progressPercentage: number;
  successMetrics?: string;
  notes?: string;
}

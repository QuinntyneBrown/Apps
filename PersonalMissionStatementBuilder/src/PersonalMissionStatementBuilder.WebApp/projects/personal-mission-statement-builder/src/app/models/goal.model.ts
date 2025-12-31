import { GoalStatus } from './goal-status.enum';

export interface Goal {
  goalId: string;
  missionStatementId?: string;
  userId: string;
  title: string;
  description?: string;
  status: GoalStatus;
  targetDate?: Date;
  completedDate?: Date;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateGoal {
  userId: string;
  missionStatementId?: string;
  title: string;
  description?: string;
  status?: GoalStatus;
  targetDate?: Date;
}

export interface UpdateGoal {
  goalId: string;
  title?: string;
  description?: string;
  status?: GoalStatus;
  targetDate?: Date;
  missionStatementId?: string;
}

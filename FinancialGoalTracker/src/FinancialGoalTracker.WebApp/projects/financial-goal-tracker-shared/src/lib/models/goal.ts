import { GoalType } from './goal-type';
import { GoalStatus } from './goal-status';

export interface Goal {
  goalId: string;
  name: string;
  description: string;
  goalType: GoalType;
  targetAmount: number;
  currentAmount: number;
  targetDate: string;
  status: GoalStatus;
  notes?: string;
  progress: number;
}

export interface CreateGoalCommand {
  name: string;
  description: string;
  goalType: GoalType;
  targetAmount: number;
  targetDate: string;
  notes?: string;
}

export interface UpdateGoalCommand {
  goalId: string;
  name: string;
  description: string;
  goalType: GoalType;
  targetAmount: number;
  targetDate: string;
  notes?: string;
}

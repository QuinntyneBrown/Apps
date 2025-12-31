import { ActivityCategory } from './activity-category';

export interface Goal {
  goalId: string;
  userId: string;
  category: ActivityCategory;
  targetHoursPerWeek: number;
  minimumHoursPerWeek?: number;
  description: string;
  isActive: boolean;
  startDate: Date;
  endDate?: Date;
  createdAt: Date;
  targetHoursPerDay: number;
}

export interface CreateGoalRequest {
  userId: string;
  category: ActivityCategory;
  targetHoursPerWeek: number;
  minimumHoursPerWeek?: number;
  description: string;
  startDate: Date;
  endDate?: Date;
}

export interface UpdateGoalRequest {
  category: ActivityCategory;
  targetHoursPerWeek: number;
  minimumHoursPerWeek?: number;
  description: string;
  isActive: boolean;
  startDate: Date;
  endDate?: Date;
}

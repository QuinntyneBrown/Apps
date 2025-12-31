export interface Goal {
  goalId: string;
  userId: string;
  dailyGoalMl: number;
  startDate: Date;
  isActive: boolean;
  notes?: string;
  createdAt: Date;
}

export interface CreateGoalCommand {
  userId: string;
  dailyGoalMl: number;
  startDate: Date;
  isActive: boolean;
  notes?: string;
}

export interface UpdateGoalCommand {
  goalId: string;
  userId: string;
  dailyGoalMl: number;
  startDate: Date;
  isActive: boolean;
  notes?: string;
}

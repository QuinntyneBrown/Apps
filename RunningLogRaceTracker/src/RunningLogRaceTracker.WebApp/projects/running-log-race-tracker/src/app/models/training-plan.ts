export interface TrainingPlan {
  trainingPlanId: string;
  userId: string;
  name: string;
  raceId: string | null;
  startDate: string;
  endDate: string;
  weeklyMileageGoal: number | null;
  planDetails: string | null;
  isActive: boolean;
  notes: string | null;
  createdAt: string;
}

export interface CreateTrainingPlanRequest {
  userId: string;
  name: string;
  raceId?: string;
  startDate: string;
  endDate: string;
  weeklyMileageGoal?: number;
  planDetails?: string;
  isActive: boolean;
  notes?: string;
}

export interface UpdateTrainingPlanRequest {
  name: string;
  raceId?: string;
  startDate: string;
  endDate: string;
  weeklyMileageGoal?: number;
  planDetails?: string;
  isActive: boolean;
  notes?: string;
}

export interface Milestone {
  milestoneId: string;
  goalId: string;
  name: string;
  targetAmount: number;
  targetDate: string;
  isCompleted: boolean;
  completedDate?: string;
  notes?: string;
}

export interface CreateMilestoneCommand {
  goalId: string;
  name: string;
  targetAmount: number;
  targetDate: string;
  notes?: string;
}

export interface UpdateMilestoneCommand {
  milestoneId: string;
  goalId: string;
  name: string;
  targetAmount: number;
  targetDate: string;
  notes?: string;
}

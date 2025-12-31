export interface Milestone {
  milestoneId: string;
  projectId: string;
  name: string;
  description?: string;
  targetDate?: string;
  isAchieved: boolean;
  achievementDate?: string;
  createdAt: string;
  isOverdue: boolean;
}

export interface CreateMilestone {
  projectId: string;
  name: string;
  description?: string;
  targetDate?: string;
}

export interface UpdateMilestone {
  milestoneId: string;
  name: string;
  description?: string;
  targetDate?: string;
  isAchieved: boolean;
}

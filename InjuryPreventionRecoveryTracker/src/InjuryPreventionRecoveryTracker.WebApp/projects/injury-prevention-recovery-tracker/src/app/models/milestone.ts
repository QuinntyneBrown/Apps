export interface Milestone {
  milestoneId: string;
  userId: string;
  injuryId: string;
  name: string;
  description?: string;
  targetDate?: string;
  achievedDate?: string;
  isAchieved: boolean;
  notes?: string;
  createdAt: string;
}

export interface CreateMilestone {
  userId: string;
  injuryId: string;
  name: string;
  description?: string;
  targetDate?: string;
  notes?: string;
}

export interface UpdateMilestone {
  milestoneId: string;
  name: string;
  description?: string;
  targetDate?: string;
  achievedDate?: string;
  isAchieved: boolean;
  notes?: string;
}

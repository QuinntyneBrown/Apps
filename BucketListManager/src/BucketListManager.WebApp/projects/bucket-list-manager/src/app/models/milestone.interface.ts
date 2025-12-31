export interface Milestone {
  milestoneId: string;
  userId: string;
  bucketListItemId: string;
  title: string;
  description?: string;
  isCompleted: boolean;
  completedDate?: string;
  createdAt: string;
}

export interface CreateMilestone {
  userId: string;
  bucketListItemId: string;
  title: string;
  description?: string;
}

export interface UpdateMilestone {
  milestoneId: string;
  title: string;
  description?: string;
  isCompleted: boolean;
  completedDate?: string;
}

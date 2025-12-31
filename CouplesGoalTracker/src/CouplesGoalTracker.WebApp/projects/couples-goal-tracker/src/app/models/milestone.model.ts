export interface Milestone {
  milestoneId: string;
  goalId: string;
  userId: string;
  title: string;
  description: string | null;
  targetDate: string | null;
  completedDate: string | null;
  isCompleted: boolean;
  sortOrder: number;
  createdAt: string;
  updatedAt: string | null;
  isOverdue: boolean;
}

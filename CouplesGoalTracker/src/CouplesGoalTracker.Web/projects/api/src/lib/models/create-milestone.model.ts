export interface CreateMilestone {
  goalId: string;
  userId: string;
  title: string;
  description: string | null;
  targetDate: string | null;
  sortOrder: number;
}

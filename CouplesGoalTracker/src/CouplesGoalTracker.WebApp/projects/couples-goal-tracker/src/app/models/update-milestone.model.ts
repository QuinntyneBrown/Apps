export interface UpdateMilestone {
  milestoneId: string;
  title: string;
  description: string | null;
  targetDate: string | null;
  isCompleted: boolean;
  sortOrder: number;
}

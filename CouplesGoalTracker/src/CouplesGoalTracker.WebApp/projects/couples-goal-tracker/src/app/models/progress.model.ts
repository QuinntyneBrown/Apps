export interface Progress {
  progressId: string;
  goalId: string;
  userId: string;
  progressDate: string;
  notes: string;
  completionPercentage: number;
  effortHours: number | null;
  isSignificant: boolean;
  createdAt: string;
  updatedAt: string | null;
}

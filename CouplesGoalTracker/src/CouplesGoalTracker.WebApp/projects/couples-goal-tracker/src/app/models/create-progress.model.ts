export interface CreateProgress {
  goalId: string;
  userId: string;
  progressDate: string;
  notes: string;
  completionPercentage: number;
  effortHours: number | null;
  isSignificant: boolean;
}

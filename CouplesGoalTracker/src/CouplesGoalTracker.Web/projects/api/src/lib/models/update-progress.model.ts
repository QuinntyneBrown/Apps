export interface UpdateProgress {
  progressId: string;
  progressDate: string;
  notes: string;
  completionPercentage: number;
  effortHours: number | null;
  isSignificant: boolean;
}

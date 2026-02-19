export interface Progress {
  progressId: string;
  goalId: string;
  userId: string;
  progressDate: Date;
  notes: string;
  completionPercentage: number;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateProgress {
  goalId: string;
  userId: string;
  progressDate?: Date;
  notes: string;
  completionPercentage: number;
}

export interface UpdateProgress {
  progressId: string;
  progressDate?: Date;
  notes?: string;
  completionPercentage?: number;
}

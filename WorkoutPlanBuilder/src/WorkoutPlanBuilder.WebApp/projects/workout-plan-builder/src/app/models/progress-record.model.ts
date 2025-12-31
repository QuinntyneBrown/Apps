export interface ProgressRecord {
  progressRecordId: string;
  userId: string;
  workoutId: string;
  actualDurationMinutes: number;
  caloriesBurned?: number;
  performanceRating?: number;
  notes?: string;
  exerciseDetails?: string;
  completedAt: Date;
  createdAt: Date;
}

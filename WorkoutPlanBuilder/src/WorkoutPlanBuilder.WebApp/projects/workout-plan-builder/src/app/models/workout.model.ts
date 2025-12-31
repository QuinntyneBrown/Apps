export interface Workout {
  workoutId: string;
  userId: string;
  name: string;
  description?: string;
  targetDurationMinutes: number;
  difficultyLevel: number;
  goal?: string;
  isTemplate: boolean;
  isActive: boolean;
  scheduledDays?: string;
  createdAt: Date;
}

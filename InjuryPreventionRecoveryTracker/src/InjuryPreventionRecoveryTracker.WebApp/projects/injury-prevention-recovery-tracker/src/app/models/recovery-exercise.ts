export interface RecoveryExercise {
  recoveryExerciseId: string;
  userId: string;
  injuryId: string;
  name: string;
  description?: string;
  frequency: string;
  setsAndReps?: string;
  durationMinutes?: number;
  instructions?: string;
  lastCompleted?: string;
  isActive: boolean;
  createdAt: string;
}

export interface CreateRecoveryExercise {
  userId: string;
  injuryId: string;
  name: string;
  description?: string;
  frequency: string;
  setsAndReps?: string;
  durationMinutes?: number;
  instructions?: string;
}

export interface UpdateRecoveryExercise {
  recoveryExerciseId: string;
  name: string;
  description?: string;
  frequency: string;
  setsAndReps?: string;
  durationMinutes?: number;
  instructions?: string;
  lastCompleted?: string;
  isActive: boolean;
}

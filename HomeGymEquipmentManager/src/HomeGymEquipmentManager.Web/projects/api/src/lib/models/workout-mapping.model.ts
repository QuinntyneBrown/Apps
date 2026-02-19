export interface WorkoutMapping {
  workoutMappingId?: string;
  userId: string;
  equipmentId: string;
  exerciseName: string;
  muscleGroup?: string;
  instructions?: string;
  isFavorite: boolean;
  createdAt?: string;
}

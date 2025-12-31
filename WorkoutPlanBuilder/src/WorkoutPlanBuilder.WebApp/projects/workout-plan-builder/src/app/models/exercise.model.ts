import { ExerciseType } from './exercise-type.enum';
import { MuscleGroup } from './muscle-group.enum';

export interface Exercise {
  exerciseId: string;
  userId: string;
  name: string;
  description?: string;
  exerciseType: ExerciseType;
  primaryMuscleGroup: MuscleGroup;
  secondaryMuscleGroups?: string;
  equipment?: string;
  difficultyLevel: number;
  videoUrl?: string;
  isCustom: boolean;
  createdAt: Date;
}

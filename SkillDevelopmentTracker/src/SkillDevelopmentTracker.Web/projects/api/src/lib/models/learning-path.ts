export interface LearningPath {
  learningPathId: string;
  userId: string;
  title: string;
  description: string;
  startDate: string;
  targetDate?: string;
  completionDate?: string;
  courseIds: string[];
  skillIds: string[];
  progressPercentage: number;
  isCompleted: boolean;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

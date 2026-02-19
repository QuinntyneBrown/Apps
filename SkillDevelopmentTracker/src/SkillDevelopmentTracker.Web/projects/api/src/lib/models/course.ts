export interface Course {
  courseId: string;
  userId: string;
  title: string;
  provider: string;
  instructor?: string;
  courseUrl?: string;
  startDate?: string;
  completionDate?: string;
  progressPercentage: number;
  estimatedHours?: number;
  actualHours: number;
  isCompleted: boolean;
  skillIds: string[];
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

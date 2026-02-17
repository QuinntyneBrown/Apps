export interface CompletionLog {
  completionLogId: string;
  routineId: string;
  userId: string;
  completionDate: string;
  actualStartTime?: string;
  actualEndTime?: string;
  tasksCompleted: number;
  totalTasks: number;
  notes?: string;
  moodRating?: number;
  createdAt: string;
}

export interface CreateCompletionLogRequest {
  routineId: string;
  userId: string;
  completionDate: string;
  actualStartTime?: string;
  actualEndTime?: string;
  tasksCompleted: number;
  totalTasks: number;
  notes?: string;
  moodRating?: number;
}

export interface UpdateCompletionLogRequest {
  completionDate: string;
  actualStartTime?: string;
  actualEndTime?: string;
  tasksCompleted: number;
  totalTasks: number;
  notes?: string;
  moodRating?: number;
}

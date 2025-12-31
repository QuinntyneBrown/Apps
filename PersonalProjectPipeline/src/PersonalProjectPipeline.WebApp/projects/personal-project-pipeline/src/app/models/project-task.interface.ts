export interface ProjectTask {
  projectTaskId: string;
  projectId: string;
  milestoneId?: string;
  title: string;
  description?: string;
  dueDate?: string;
  isCompleted: boolean;
  completionDate?: string;
  estimatedHours?: number;
  createdAt: string;
}

export interface CreateProjectTask {
  projectId: string;
  milestoneId?: string;
  title: string;
  description?: string;
  dueDate?: string;
  estimatedHours?: number;
}

export interface UpdateProjectTask {
  projectTaskId: string;
  title: string;
  description?: string;
  dueDate?: string;
  isCompleted: boolean;
  estimatedHours?: number;
}

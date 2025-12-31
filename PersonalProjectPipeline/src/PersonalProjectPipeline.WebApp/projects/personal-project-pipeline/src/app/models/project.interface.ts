import { ProjectStatus } from './project-status.enum';
import { ProjectPriority } from './project-priority.enum';

export interface Project {
  projectId: string;
  userId: string;
  name: string;
  description?: string;
  status: ProjectStatus;
  priority: ProjectPriority;
  startDate?: string;
  targetDate?: string;
  completionDate?: string;
  tags?: string;
  createdAt: string;
  progressPercentage: number;
}

export interface CreateProject {
  userId: string;
  name: string;
  description?: string;
  status: ProjectStatus;
  priority: ProjectPriority;
  startDate?: string;
  targetDate?: string;
  tags?: string;
}

export interface UpdateProject {
  projectId: string;
  name: string;
  description?: string;
  status: ProjectStatus;
  priority: ProjectPriority;
  startDate?: string;
  targetDate?: string;
  tags?: string;
}

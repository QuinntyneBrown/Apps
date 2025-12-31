import { ProjectStatus } from './project-status';

export interface Project {
  projectId: string;
  userId: string;
  name: string;
  description?: string;
  status: ProjectStatus;
  startDate?: string;
  endDate?: string;
  estimatedCost?: number;
  actualCost?: number;
  createdAt: string;
}

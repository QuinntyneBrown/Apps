import { ProjectStatus } from './project-status.enum';
import { WoodType } from './wood-type.enum';

export interface Project {
  projectId: string;
  userId: string;
  name: string;
  description: string;
  status: ProjectStatus;
  woodType: WoodType;
  startDate?: string;
  completionDate?: string;
  estimatedCost?: number;
  actualCost?: number;
  notes?: string;
  createdAt: string;
}

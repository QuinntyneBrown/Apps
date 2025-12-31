import { ProjectPhase } from './project-phase.enum';

export interface Project {
  projectId: string;
  userId: string;
  carMake: string;
  carModel: string;
  year?: number;
  phase: ProjectPhase;
  startDate: string;
  completionDate?: string;
  estimatedBudget?: number;
  actualCost?: number;
  notes?: string;
  createdAt: string;
}

export interface CreateProjectCommand {
  userId: string;
  carMake: string;
  carModel: string;
  year?: number;
  phase: ProjectPhase;
  startDate?: string;
  estimatedBudget?: number;
  notes?: string;
}

export interface UpdateProjectCommand {
  projectId: string;
  userId: string;
  carMake: string;
  carModel: string;
  year?: number;
  phase: ProjectPhase;
  startDate: string;
  completionDate?: string;
  estimatedBudget?: number;
  actualCost?: number;
  notes?: string;
}

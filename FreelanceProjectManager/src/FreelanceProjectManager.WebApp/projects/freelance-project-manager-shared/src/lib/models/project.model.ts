import { ProjectStatus } from './enums';

export interface Project {
  projectId: string;
  userId: string;
  clientId: string;
  name: string;
  description: string;
  status: ProjectStatus;
  startDate: Date;
  dueDate?: Date;
  completionDate?: Date;
  hourlyRate?: number;
  fixedBudget?: number;
  currency: string;
  notes?: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateProjectRequest {
  userId: string;
  clientId: string;
  name: string;
  description: string;
  status: ProjectStatus;
  startDate: Date;
  dueDate?: Date;
  hourlyRate?: number;
  fixedBudget?: number;
  currency?: string;
  notes?: string;
}

export interface UpdateProjectRequest {
  projectId: string;
  userId: string;
  clientId: string;
  name: string;
  description: string;
  status: ProjectStatus;
  startDate: Date;
  dueDate?: Date;
  completionDate?: Date;
  hourlyRate?: number;
  fixedBudget?: number;
  currency: string;
  notes?: string;
}

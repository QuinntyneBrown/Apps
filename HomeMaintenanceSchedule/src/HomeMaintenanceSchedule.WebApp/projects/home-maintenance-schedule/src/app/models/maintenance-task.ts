import { MaintenanceType } from './maintenance-type';
import { TaskStatus } from './task-status';

export interface MaintenanceTask {
  maintenanceTaskId: string;
  userId: string;
  name: string;
  description?: string;
  maintenanceType: MaintenanceType;
  status: TaskStatus;
  dueDate?: string;
  completedDate?: string;
  recurrenceFrequencyDays?: number;
  estimatedCost?: number;
  actualCost?: number;
  priority: number;
  location?: string;
  contractorId?: string;
  createdAt: string;
  updatedAt: string;
}

export interface CreateMaintenanceTask {
  userId: string;
  name: string;
  description?: string;
  maintenanceType: MaintenanceType;
  dueDate?: string;
  recurrenceFrequencyDays?: number;
  estimatedCost?: number;
  priority: number;
  location?: string;
  contractorId?: string;
}

export interface UpdateMaintenanceTask {
  maintenanceTaskId: string;
  name: string;
  description?: string;
  maintenanceType: MaintenanceType;
  status: TaskStatus;
  dueDate?: string;
  completedDate?: string;
  recurrenceFrequencyDays?: number;
  estimatedCost?: number;
  actualCost?: number;
  priority: number;
  location?: string;
  contractorId?: string;
}

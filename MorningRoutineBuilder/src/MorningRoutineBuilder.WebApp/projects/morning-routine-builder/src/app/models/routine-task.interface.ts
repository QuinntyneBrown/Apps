import { TaskType } from './task-type.enum';

export interface RoutineTask {
  routineTaskId: string;
  routineId: string;
  name: string;
  taskType: TaskType;
  description?: string;
  estimatedDurationMinutes: number;
  sortOrder: number;
  isOptional: boolean;
  createdAt: string;
}

export interface CreateRoutineTaskRequest {
  routineId: string;
  name: string;
  taskType: TaskType;
  description?: string;
  estimatedDurationMinutes: number;
  sortOrder: number;
  isOptional: boolean;
}

export interface UpdateRoutineTaskRequest {
  name: string;
  taskType: TaskType;
  description?: string;
  estimatedDurationMinutes: number;
  sortOrder: number;
  isOptional: boolean;
}

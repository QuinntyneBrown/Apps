export interface Routine {
  routineId: string;
  userId: string;
  name: string;
  description?: string;
  targetStartTime: string;
  estimatedDurationMinutes: number;
  isActive: boolean;
  createdAt: string;
}

export interface CreateRoutineRequest {
  userId: string;
  name: string;
  description?: string;
  targetStartTime: string;
  estimatedDurationMinutes: number;
}

export interface UpdateRoutineRequest {
  name: string;
  description?: string;
  targetStartTime: string;
  estimatedDurationMinutes: number;
  isActive: boolean;
}

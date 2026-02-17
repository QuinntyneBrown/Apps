import { ChoreFrequency } from './chore-frequency';

export interface Chore {
  choreId: string;
  userId: string;
  name: string;
  description?: string;
  frequency: ChoreFrequency;
  estimatedMinutes?: number;
  points: number;
  category?: string;
  isActive: boolean;
  createdAt: Date;
}

export interface CreateChore {
  userId: string;
  name: string;
  description?: string;
  frequency: ChoreFrequency;
  estimatedMinutes?: number;
  points: number;
  category?: string;
}

export interface UpdateChore {
  name: string;
  description?: string;
  frequency: ChoreFrequency;
  estimatedMinutes?: number;
  points: number;
  category?: string;
  isActive: boolean;
}

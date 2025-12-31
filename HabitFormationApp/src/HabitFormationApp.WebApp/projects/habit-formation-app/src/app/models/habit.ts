import { HabitFrequency } from './habit-frequency';

export interface Habit {
  habitId: string;
  userId: string;
  name: string;
  description?: string;
  frequency: HabitFrequency;
  targetDaysPerWeek: number;
  startDate: string;
  isActive: boolean;
  notes?: string;
  createdAt: string;
}

export interface CreateHabitRequest {
  userId: string;
  name: string;
  description?: string;
  frequency: HabitFrequency;
  targetDaysPerWeek: number;
  startDate: string;
  notes?: string;
}

export interface UpdateHabitRequest {
  habitId: string;
  name: string;
  description?: string;
  frequency: HabitFrequency;
  targetDaysPerWeek: number;
  notes?: string;
}

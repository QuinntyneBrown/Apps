export interface Habit {
  habitId: string;
  userId: string;
  name: string;
  description?: string;
  habitType: string;
  isPositive: boolean;
  typicalTime?: string;
  impactLevel: number;
  isActive: boolean;
  createdAt: string;
  isHighImpact: boolean;
}

export interface CreateHabitRequest {
  userId: string;
  name: string;
  description?: string;
  habitType: string;
  isPositive: boolean;
  typicalTime?: string;
  impactLevel: number;
}

export interface UpdateHabitRequest {
  habitId: string;
  name: string;
  description?: string;
  habitType: string;
  isPositive: boolean;
  typicalTime?: string;
  impactLevel: number;
  isActive: boolean;
}

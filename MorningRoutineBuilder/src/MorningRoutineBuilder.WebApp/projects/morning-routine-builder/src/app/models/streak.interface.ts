export interface Streak {
  streakId: string;
  routineId: string;
  userId: string;
  currentStreak: number;
  longestStreak: number;
  lastCompletionDate?: string;
  streakStartDate?: string;
  isActive: boolean;
  createdAt: string;
}

export interface CreateStreakRequest {
  routineId: string;
  userId: string;
}

export interface UpdateStreakRequest {
  currentStreak: number;
  longestStreak: number;
  lastCompletionDate?: string;
  streakStartDate?: string;
  isActive: boolean;
}

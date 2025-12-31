export interface Streak {
  streakId: string;
  habitId: string;
  currentStreak: number;
  longestStreak: number;
  lastCompletedDate?: string;
  createdAt: string;
}

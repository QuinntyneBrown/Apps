export interface Achievement {
  achievementId: string;
  userId: string;
  reviewPeriodId: string;
  title: string;
  description: string;
  achievedDate: string;
  impact?: string;
  category?: string;
  isKeyAchievement: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateAchievement {
  userId: string;
  reviewPeriodId: string;
  title: string;
  description: string;
  achievedDate: string;
  impact?: string;
  category?: string;
  isKeyAchievement: boolean;
}

export interface UpdateAchievement {
  achievementId: string;
  title: string;
  description: string;
  achievedDate: string;
  impact?: string;
  category?: string;
  isKeyAchievement: boolean;
}

import { AchievementType } from './achievement-type.enum';

export interface Achievement {
  achievementId: string;
  userId: string;
  title: string;
  description: string;
  achievementType: AchievementType;
  achievedDate: string;
  organization?: string;
  impact?: string;
  skillIds: string[];
  projectIds: string[];
  isFeatured: boolean;
  tags: string[];
  createdAt: string;
  updatedAt?: string;
}

export interface CreateAchievement {
  userId: string;
  title: string;
  description: string;
  achievementType: AchievementType;
  achievedDate: string;
  organization?: string;
  impact?: string;
  skillIds?: string[];
  projectIds?: string[];
  tags?: string[];
}

export interface UpdateAchievement {
  achievementId: string;
  title: string;
  description: string;
  achievementType: AchievementType;
  achievedDate: string;
  organization?: string;
  impact?: string;
  skillIds?: string[];
  projectIds?: string[];
  tags?: string[];
}

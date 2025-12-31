export enum AchievementType {
  Award = 0,
  Certification = 1,
  Publication = 2,
  Presentation = 3,
  ProjectMilestone = 4,
  Promotion = 5,
  FinancialImpact = 6,
  Leadership = 7,
  Innovation = 8,
  Other = 9
}

export const AchievementTypeLabels: Record<AchievementType, string> = {
  [AchievementType.Award]: 'Award',
  [AchievementType.Certification]: 'Certification',
  [AchievementType.Publication]: 'Publication',
  [AchievementType.Presentation]: 'Presentation',
  [AchievementType.ProjectMilestone]: 'Project Milestone',
  [AchievementType.Promotion]: 'Promotion',
  [AchievementType.FinancialImpact]: 'Financial Impact',
  [AchievementType.Leadership]: 'Leadership',
  [AchievementType.Innovation]: 'Innovation',
  [AchievementType.Other]: 'Other'
};

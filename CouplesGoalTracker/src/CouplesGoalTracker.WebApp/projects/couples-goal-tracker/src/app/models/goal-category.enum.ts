export enum GoalCategory {
  Communication = 0,
  Intimacy = 1,
  Financial = 2,
  HealthAndWellness = 3,
  AdventureAndTravel = 4,
  PersonalGrowth = 5,
  FamilyPlanning = 6,
  QualityTime = 7,
  CareerAndEducation = 8,
  Other = 9
}

export const GoalCategoryLabels: Record<GoalCategory, string> = {
  [GoalCategory.Communication]: 'Communication',
  [GoalCategory.Intimacy]: 'Intimacy',
  [GoalCategory.Financial]: 'Financial',
  [GoalCategory.HealthAndWellness]: 'Health & Wellness',
  [GoalCategory.AdventureAndTravel]: 'Adventure & Travel',
  [GoalCategory.PersonalGrowth]: 'Personal Growth',
  [GoalCategory.FamilyPlanning]: 'Family Planning',
  [GoalCategory.QualityTime]: 'Quality Time',
  [GoalCategory.CareerAndEducation]: 'Career & Education',
  [GoalCategory.Other]: 'Other'
};

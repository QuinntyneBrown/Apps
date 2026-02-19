export enum LessonCategory {
  PersonalDevelopment = 0,
  Professional = 1,
  Health = 2,
  Relationships = 3,
  Finance = 4,
  Leadership = 5,
  Technical = 6,
  LifeWisdom = 7,
  Other = 8
}

export const LessonCategoryLabels: Record<LessonCategory, string> = {
  [LessonCategory.PersonalDevelopment]: 'Personal Development',
  [LessonCategory.Professional]: 'Professional',
  [LessonCategory.Health]: 'Health',
  [LessonCategory.Relationships]: 'Relationships',
  [LessonCategory.Finance]: 'Finance',
  [LessonCategory.Leadership]: 'Leadership',
  [LessonCategory.Technical]: 'Technical',
  [LessonCategory.LifeWisdom]: 'Life Wisdom',
  [LessonCategory.Other]: 'Other'
};

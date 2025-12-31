export enum TopicCategory {
  FaithAndSpirituality = 0,
  RelationshipsAndFamily = 1,
  CareerAndWork = 2,
  PersonalGrowth = 3,
  MentalHealth = 4,
  PhysicalHealth = 5,
  Financial = 6,
  CommunityAndService = 7,
  Leadership = 8,
  Other = 9
}

export const TopicCategoryLabels: Record<TopicCategory, string> = {
  [TopicCategory.FaithAndSpirituality]: 'Faith & Spirituality',
  [TopicCategory.RelationshipsAndFamily]: 'Relationships & Family',
  [TopicCategory.CareerAndWork]: 'Career & Work',
  [TopicCategory.PersonalGrowth]: 'Personal Growth',
  [TopicCategory.MentalHealth]: 'Mental Health',
  [TopicCategory.PhysicalHealth]: 'Physical Health',
  [TopicCategory.Financial]: 'Financial',
  [TopicCategory.CommunityAndService]: 'Community & Service',
  [TopicCategory.Leadership]: 'Leadership',
  [TopicCategory.Other]: 'Other'
};

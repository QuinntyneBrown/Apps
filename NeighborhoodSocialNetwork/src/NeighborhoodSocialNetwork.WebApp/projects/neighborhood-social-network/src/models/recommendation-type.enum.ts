export enum RecommendationType {
  Restaurant = 0,
  ServiceProvider = 1,
  Shop = 2,
  Healthcare = 3,
  Entertainment = 4,
  Education = 5,
  Other = 6
}

export const RecommendationTypeLabels: Record<RecommendationType, string> = {
  [RecommendationType.Restaurant]: 'Restaurant',
  [RecommendationType.ServiceProvider]: 'Service Provider',
  [RecommendationType.Shop]: 'Shop',
  [RecommendationType.Healthcare]: 'Healthcare',
  [RecommendationType.Entertainment]: 'Entertainment',
  [RecommendationType.Education]: 'Education',
  [RecommendationType.Other]: 'Other'
};

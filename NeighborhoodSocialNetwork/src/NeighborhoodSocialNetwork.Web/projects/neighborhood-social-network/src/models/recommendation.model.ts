import { RecommendationType } from './recommendation-type.enum';

export interface Recommendation {
  recommendationId: string;
  neighborId: string;
  title: string;
  description: string;
  recommendationType: RecommendationType;
  businessName?: string;
  location?: string;
  rating?: number;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateRecommendation {
  neighborId: string;
  title: string;
  description: string;
  recommendationType: RecommendationType;
  businessName?: string;
  location?: string;
  rating?: number;
}

export interface UpdateRecommendation {
  recommendationId: string;
  title: string;
  description: string;
  recommendationType: RecommendationType;
  businessName?: string;
  location?: string;
  rating?: number;
}

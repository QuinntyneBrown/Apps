import { Genre } from './genre';
import { ContentType } from './content-type';

export interface Recommendation {
  recommendationId: string;
  title: string;
  contentType: ContentType;
  releaseYear: number;
  genres: Genre[];
  matchScore: number;
  reason: string;
  source: 'system' | 'friend' | 'critic';
  posterUrl?: string;
}

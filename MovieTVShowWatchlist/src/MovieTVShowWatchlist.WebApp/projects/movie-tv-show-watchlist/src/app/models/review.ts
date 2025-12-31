export interface Review {
  reviewId: string;
  contentId: string;
  contentType: string;
  reviewText: string;
  hasSpoilers: boolean;
  reviewDate: Date;
  wouldRecommend: boolean;
  targetAudience?: string;
  themes: string[];
}

export interface CreateReviewRequest {
  contentId: string;
  contentType: string;
  reviewText: string;
  hasSpoilers: boolean;
  themesDiscussed?: string[];
  wouldRecommend: boolean;
  targetAudience?: string;
}

export interface UpdateReviewRequest {
  reviewText: string;
  hasSpoilers: boolean;
  themesDiscussed?: string[];
  wouldRecommend: boolean;
  targetAudience?: string;
}

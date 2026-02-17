export interface Rating {
  ratingId: string;
  contentId: string;
  contentType: string;
  ratingValue: number;
  ratingScale: string;
  ratingDate: Date;
  viewingDate?: Date;
  isRewatchRating: boolean;
  mood?: string;
}

export interface CreateRatingRequest {
  ratingValue: number;
  ratingScale: string;
  ratingDate: Date;
  viewingDate?: Date;
  isRewatchRating: boolean;
  mood?: string;
}

export interface UpdateRatingRequest {
  ratingValue: number;
  ratingScale: string;
  ratingDate: Date;
  viewingDate?: Date;
  isRewatchRating: boolean;
  mood?: string;
}

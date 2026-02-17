export interface Review {
  reviewId: string;
  userId: string;
  campsiteId: string;
  rating: number;
  reviewText?: string;
  reviewDate: Date;
  createdAt: Date;
}

export interface CreateReview {
  userId: string;
  campsiteId: string;
  rating: number;
  reviewText?: string;
}

export interface UpdateReview {
  reviewId: string;
  userId: string;
  campsiteId: string;
  rating: number;
  reviewText?: string;
}

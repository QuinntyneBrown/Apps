export interface Review {
  reviewId: string;
  userId: string;
  bookId: string;
  rating: number;
  reviewText: string;
  isRecommended: boolean;
  reviewDate: string;
  createdAt: string;
}

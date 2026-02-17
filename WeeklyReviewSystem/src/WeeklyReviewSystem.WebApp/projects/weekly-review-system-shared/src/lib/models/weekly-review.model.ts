export interface WeeklyReview {
  weeklyReviewId: string;
  userId: string;
  weekStartDate: string;
  weekEndDate: string;
  overallRating?: number;
  reflections?: string;
  lessonsLearned?: string;
  gratitude?: string;
  improvementAreas?: string;
  isCompleted: boolean;
  createdAt: string;
}

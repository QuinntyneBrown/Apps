export interface Challenge {
  challengeId: string;
  weeklyReviewId: string;
  title: string;
  description?: string;
  resolution?: string;
  isResolved: boolean;
  lessonsLearned?: string;
  createdAt: string;
}

export interface Accomplishment {
  accomplishmentId: string;
  weeklyReviewId: string;
  title: string;
  description?: string;
  category?: string;
  impactLevel?: number;
  createdAt: string;
}

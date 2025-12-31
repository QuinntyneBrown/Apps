export interface ReviewPeriod {
  reviewPeriodId: string;
  userId: string;
  title: string;
  startDate: string;
  endDate: string;
  reviewDueDate?: string;
  reviewerName?: string;
  isCompleted: boolean;
  completedDate?: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateReviewPeriod {
  userId: string;
  title: string;
  startDate: string;
  endDate: string;
  reviewDueDate?: string;
  reviewerName?: string;
  notes?: string;
}

export interface UpdateReviewPeriod {
  reviewPeriodId: string;
  title: string;
  startDate: string;
  endDate: string;
  reviewDueDate?: string;
  reviewerName?: string;
  isCompleted: boolean;
  notes?: string;
}

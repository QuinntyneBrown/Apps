export interface Feedback {
  feedbackId: string;
  userId: string;
  reviewPeriodId: string;
  source: string;
  content: string;
  receivedDate: string;
  feedbackType?: string;
  category?: string;
  isKeyFeedback: boolean;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateFeedback {
  userId: string;
  reviewPeriodId: string;
  source: string;
  content: string;
  receivedDate: string;
  feedbackType?: string;
  category?: string;
  isKeyFeedback: boolean;
  notes?: string;
}

export interface UpdateFeedback {
  feedbackId: string;
  source: string;
  content: string;
  receivedDate: string;
  feedbackType?: string;
  category?: string;
  isKeyFeedback: boolean;
  notes?: string;
}

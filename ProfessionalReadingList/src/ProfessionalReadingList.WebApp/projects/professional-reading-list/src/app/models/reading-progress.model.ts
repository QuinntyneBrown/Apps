export interface ReadingProgress {
  readingProgressId: string;
  userId: string;
  resourceId: string;
  status: string;
  currentPage?: number;
  progressPercentage: number;
  startDate?: string;
  completionDate?: string;
  rating?: number;
  review?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateReadingProgressCommand {
  userId: string;
  resourceId: string;
  status: string;
  currentPage?: number;
  progressPercentage: number;
}

export interface UpdateReadingProgressCommand {
  readingProgressId: string;
  status: string;
  currentPage?: number;
  progressPercentage: number;
  rating?: number;
  review?: string;
}

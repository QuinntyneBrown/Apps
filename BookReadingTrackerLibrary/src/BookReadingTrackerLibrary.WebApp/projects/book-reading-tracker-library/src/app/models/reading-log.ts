export interface ReadingLog {
  readingLogId: string;
  userId: string;
  bookId: string;
  startPage: number;
  endPage: number;
  startTime: string;
  endTime?: string;
  notes?: string;
  createdAt: string;
  pagesRead: number;
  durationInMinutes: number;
}

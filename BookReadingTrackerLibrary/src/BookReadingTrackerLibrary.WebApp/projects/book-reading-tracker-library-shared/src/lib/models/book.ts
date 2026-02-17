import { Genre } from './genre';
import { ReadingStatus } from './reading-status';

export interface Book {
  bookId: string;
  userId: string;
  title: string;
  author: string;
  isbn?: string;
  genre: Genre;
  status: ReadingStatus;
  totalPages: number;
  currentPage: number;
  startDate?: string;
  finishDate?: string;
  createdAt: string;
  progressPercentage: number;
}

import { Genre } from './genre';

export interface Wishlist {
  wishlistId: string;
  userId: string;
  title: string;
  author: string;
  isbn?: string;
  genre?: Genre;
  priority: number;
  notes?: string;
  isAcquired: boolean;
  createdAt: string;
}

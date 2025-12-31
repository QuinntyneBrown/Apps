import { Platform } from './platform.enum';
import { Genre } from './genre.enum';

export interface Wishlist {
  wishlistId: string;
  userId: string;
  title: string;
  platform?: Platform;
  genre?: Genre;
  priority: number;
  notes?: string;
  isAcquired: boolean;
  createdAt: string;
}

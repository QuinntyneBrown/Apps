import { Platform } from './platform.enum';
import { Genre } from './genre.enum';
import { CompletionStatus } from './completion-status.enum';

export interface Game {
  gameId: string;
  userId: string;
  title: string;
  platform: Platform;
  genre: Genre;
  status: CompletionStatus;
  publisher?: string;
  developer?: string;
  releaseDate?: string;
  purchaseDate?: string;
  purchasePrice?: number;
  rating?: number;
  notes?: string;
  createdAt: string;
}

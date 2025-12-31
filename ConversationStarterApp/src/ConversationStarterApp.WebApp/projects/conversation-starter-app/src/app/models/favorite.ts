import { Prompt } from './prompt';

export interface Favorite {
  favoriteId: string;
  promptId: string;
  userId: string;
  notes?: string;
  createdAt: Date;
  prompt?: Prompt;
}

export interface CreateFavoriteRequest {
  userId: string;
  promptId: string;
  notes?: string;
}

export interface UpdateFavoriteRequest {
  favoriteId: string;
  notes?: string;
}

import { Occasion } from './occasion';
import { Recipient } from './recipient';

export interface GiftIdea {
  giftIdeaId: string;
  userId: string;
  recipientId: string | null;
  recipient?: Recipient;
  name: string;
  occasion: Occasion;
  estimatedPrice: number | null;
  isPurchased: boolean;
  createdAt: string;
}

export interface CreateGiftIdeaRequest {
  recipientId?: string;
  name: string;
  occasion: Occasion;
  estimatedPrice?: number;
}

export interface UpdateGiftIdeaRequest {
  giftIdeaId: string;
  recipientId?: string;
  name: string;
  occasion: Occasion;
  estimatedPrice?: number;
}

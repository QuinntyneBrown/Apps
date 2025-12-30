import { GiftIdea } from './gift-idea';

export interface Recipient {
  recipientId: string;
  userId: string;
  name: string;
  relationship: string | null;
  createdAt: string;
  giftIdeas?: GiftIdea[];
}

export interface CreateRecipientRequest {
  name: string;
  relationship?: string;
}

export interface UpdateRecipientRequest {
  recipientId: string;
  name: string;
  relationship?: string;
}

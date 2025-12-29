import { GiftStatus } from './gift-status';

export interface Gift {
  giftId: string;
  dateId: string;
  description: string;
  estimatedPrice: number;
  actualPrice: number | null;
  purchaseUrl: string;
  status: GiftStatus;
  purchasedAt: Date | null;
}

export interface Reward {
  rewardId: string;
  userId: string;
  name: string;
  description?: string;
  pointCost: number;
  category?: string;
  isAvailable: boolean;
  redeemedByFamilyMemberId?: string;
  redeemedDate?: Date;
  createdAt: Date;
}

export interface CreateReward {
  userId: string;
  name: string;
  description?: string;
  pointCost: number;
  category?: string;
}

export interface UpdateReward {
  name: string;
  description?: string;
  pointCost: number;
  category?: string;
  isAvailable: boolean;
}

export interface RedeemReward {
  familyMemberId: string;
}

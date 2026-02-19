export interface FamilyMember {
  familyMemberId: string;
  userId: string;
  name: string;
  age?: number;
  avatar?: string;
  totalPoints: number;
  availablePoints: number;
  isActive: boolean;
  createdAt: Date;
  completionRate: number;
}

export interface CreateFamilyMember {
  userId: string;
  name: string;
  age?: number;
  avatar?: string;
}

export interface UpdateFamilyMember {
  name: string;
  age?: number;
  avatar?: string;
  isActive: boolean;
}

export interface Member {
  memberId: string;
  groupId: string;
  userId: string;
  name: string;
  email?: string;
  isAdmin: boolean;
  isActive: boolean;
  joinedAt: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateMember {
  groupId: string;
  userId: string;
  name: string;
  email?: string;
  isAdmin: boolean;
}

export interface UpdateMember {
  name?: string;
  email?: string;
}

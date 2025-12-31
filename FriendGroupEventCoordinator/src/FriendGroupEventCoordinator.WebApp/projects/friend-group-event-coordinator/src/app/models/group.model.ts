export interface Group {
  groupId: string;
  createdByUserId: string;
  name: string;
  description?: string;
  isActive: boolean;
  createdAt: string;
  updatedAt?: string;
  activeMemberCount: number;
}

export interface CreateGroup {
  createdByUserId: string;
  name: string;
  description?: string;
}

export interface UpdateGroup {
  name?: string;
  description?: string;
}

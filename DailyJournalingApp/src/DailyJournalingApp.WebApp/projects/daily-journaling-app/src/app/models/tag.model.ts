export interface Tag {
  tagId: string;
  userId: string;
  name: string;
  color?: string;
  usageCount: number;
  createdAt: string;
}

export interface CreateTag {
  userId: string;
  name: string;
  color?: string;
}

export interface UpdateTag {
  tagId: string;
  name: string;
  color?: string;
}

export interface Memory {
  memoryId: string;
  userId: string;
  bucketListItemId: string;
  title: string;
  description?: string;
  memoryDate: string;
  photoUrl?: string;
  createdAt: string;
}

export interface CreateMemory {
  userId: string;
  bucketListItemId: string;
  title: string;
  description?: string;
  memoryDate?: string;
  photoUrl?: string;
}

export interface UpdateMemory {
  memoryId: string;
  title: string;
  description?: string;
  memoryDate: string;
  photoUrl?: string;
}

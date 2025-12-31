export interface Reflection {
  reflectionId: string;
  journalEntryId?: string;
  userId: string;
  text: string;
  topic?: string;
  reflectionDate: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateReflection {
  journalEntryId?: string;
  userId: string;
  text: string;
  topic?: string;
  reflectionDate: string;
}

export interface UpdateReflection {
  reflectionId: string;
  text: string;
  topic?: string;
  reflectionDate: string;
}

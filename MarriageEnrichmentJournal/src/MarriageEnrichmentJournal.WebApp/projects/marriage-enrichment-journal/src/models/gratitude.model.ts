export interface Gratitude {
  gratitudeId: string;
  journalEntryId?: string;
  userId: string;
  text: string;
  gratitudeDate: string;
  createdAt: string;
}

export interface CreateGratitude {
  journalEntryId?: string;
  userId: string;
  text: string;
  gratitudeDate: string;
}

export interface UpdateGratitude {
  gratitudeId: string;
  text: string;
  gratitudeDate: string;
}

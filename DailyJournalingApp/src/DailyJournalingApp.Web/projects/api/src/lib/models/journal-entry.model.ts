import { Mood } from './mood.enum';

export interface JournalEntry {
  journalEntryId: string;
  userId: string;
  title: string;
  content: string;
  entryDate: string;
  mood: Mood;
  promptId?: string;
  tags?: string;
  isFavorite: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateJournalEntry {
  userId: string;
  title: string;
  content: string;
  entryDate: string;
  mood: Mood;
  promptId?: string;
  tags?: string;
}

export interface UpdateJournalEntry {
  journalEntryId: string;
  title: string;
  content: string;
  entryDate: string;
  mood: Mood;
  tags?: string;
}

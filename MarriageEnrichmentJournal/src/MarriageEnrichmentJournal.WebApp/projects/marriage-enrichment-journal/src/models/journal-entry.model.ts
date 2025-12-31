import { EntryType } from './entry-type.enum';

export interface JournalEntry {
  journalEntryId: string;
  userId: string;
  title: string;
  content: string;
  entryType: EntryType;
  entryDate: string;
  isSharedWithPartner: boolean;
  isPrivate: boolean;
  tags?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateJournalEntry {
  userId: string;
  title: string;
  content: string;
  entryType: EntryType;
  entryDate: string;
  isSharedWithPartner: boolean;
  isPrivate: boolean;
  tags?: string;
}

export interface UpdateJournalEntry {
  journalEntryId: string;
  title: string;
  content: string;
  entryType: EntryType;
  entryDate: string;
  isSharedWithPartner: boolean;
  isPrivate: boolean;
  tags?: string;
}

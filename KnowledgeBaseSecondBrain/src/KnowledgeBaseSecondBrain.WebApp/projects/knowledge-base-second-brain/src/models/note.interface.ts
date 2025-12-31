import { NoteType } from './note-type.enum';

export interface Note {
  noteId: string;
  userId: string;
  title: string;
  content: string;
  noteType: NoteType;
  parentNoteId: string | null;
  isFavorite: boolean;
  isArchived: boolean;
  lastModifiedAt: string;
  createdAt: string;
}

export interface CreateNoteCommand {
  userId: string;
  title: string;
  content: string;
  noteType: NoteType;
  parentNoteId: string | null;
}

export interface UpdateNoteCommand {
  noteId: string;
  title: string;
  content: string;
  noteType: NoteType;
  parentNoteId: string | null;
  isFavorite: boolean;
  isArchived: boolean;
}

export interface Note {
  noteId: string;
  userId: string;
  meetingId: string;
  content: string;
  category?: string;
  isImportant: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateNoteDto {
  userId: string;
  meetingId: string;
  content: string;
  category?: string;
  isImportant: boolean;
}

export interface UpdateNoteDto {
  noteId: string;
  content: string;
  category?: string;
  isImportant: boolean;
}

export interface Note {
  noteId: string;
  userId: string;
  eventId: string;
  content: string;
  category?: string;
  tags: string[];
  createdAt: string;
  updatedAt?: string;
}

export interface CreateNoteCommand {
  eventId: string;
  content: string;
  category?: string;
  tags: string[];
}

export interface UpdateNoteCommand {
  noteId: string;
  eventId: string;
  content: string;
  category?: string;
  tags: string[];
}

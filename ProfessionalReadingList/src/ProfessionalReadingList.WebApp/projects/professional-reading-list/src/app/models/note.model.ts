export interface Note {
  noteId: string;
  userId: string;
  resourceId: string;
  content: string;
  pageReference?: string;
  quote?: string;
  tags: string[];
  createdAt: string;
  updatedAt?: string;
}

export interface CreateNoteCommand {
  userId: string;
  resourceId: string;
  content: string;
  pageReference?: string;
  quote?: string;
  tags: string[];
}

export interface UpdateNoteCommand {
  noteId: string;
  content: string;
  pageReference?: string;
  quote?: string;
  tags: string[];
}

export interface NoteLink {
  noteLinkId: string;
  sourceNoteId: string;
  targetNoteId: string;
  description: string | null;
  linkType: string | null;
  createdAt: string;
}

export interface CreateNoteLinkCommand {
  sourceNoteId: string;
  targetNoteId: string;
  description: string | null;
  linkType: string | null;
}

export interface UpdateNoteLinkCommand {
  noteLinkId: string;
  description: string | null;
  linkType: string | null;
}

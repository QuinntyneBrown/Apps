export interface Tag {
  tagId: string;
  userId: string;
  name: string;
  color: string | null;
  createdAt: string;
}

export interface CreateTagCommand {
  userId: string;
  name: string;
  color: string | null;
}

export interface UpdateTagCommand {
  tagId: string;
  name: string;
  color: string | null;
}

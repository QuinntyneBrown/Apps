export interface Tag {
  tagId: string;
  userId: string;
  name: string;
  createdAt: Date;
  photoCount: number;
}

export interface CreateTagCommand {
  userId: string;
  name: string;
}

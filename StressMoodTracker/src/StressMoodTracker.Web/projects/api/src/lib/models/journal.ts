export interface Journal {
  journalId: string;
  userId: string;
  title: string;
  content: string;
  entryDate: Date;
  tags?: string;
  createdAt: Date;
}

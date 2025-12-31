export interface Source {
  sourceId: string;
  userId: string;
  title: string;
  author?: string;
  sourceType: string;
  url?: string;
  dateConsumed?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateSource {
  userId: string;
  title: string;
  author?: string;
  sourceType: string;
  url?: string;
  dateConsumed?: string;
  notes?: string;
}

export interface UpdateSource {
  sourceId: string;
  title: string;
  author?: string;
  sourceType: string;
  url?: string;
  dateConsumed?: string;
  notes?: string;
}

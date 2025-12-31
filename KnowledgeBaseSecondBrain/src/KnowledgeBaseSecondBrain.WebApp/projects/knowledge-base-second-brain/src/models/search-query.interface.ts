export interface SearchQuery {
  searchQueryId: string;
  userId: string;
  queryText: string;
  name: string | null;
  isSaved: boolean;
  executionCount: number;
  lastExecutedAt: string | null;
  createdAt: string;
}

export interface CreateSearchQueryCommand {
  userId: string;
  queryText: string;
  name: string | null;
  isSaved: boolean;
}

export interface UpdateSearchQueryCommand {
  searchQueryId: string;
  queryText: string;
  name: string | null;
  isSaved: boolean;
}

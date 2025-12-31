export interface PageRevision {
  pageRevisionId: string;
  wikiPageId: string;
  version: number;
  content: string;
  changeSummary?: string;
  revisedBy?: string;
  createdAt: string;
}

export interface CreatePageRevision {
  wikiPageId: string;
  version: number;
  content: string;
  changeSummary?: string;
  revisedBy?: string;
}

export interface UpdatePageRevision {
  pageRevisionId: string;
  content: string;
  changeSummary?: string;
  revisedBy?: string;
}

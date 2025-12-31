export interface PageLink {
  pageLinkId: string;
  sourcePageId: string;
  targetPageId: string;
  anchorText?: string;
  createdAt: string;
}

export interface CreatePageLink {
  sourcePageId: string;
  targetPageId: string;
  anchorText?: string;
}

export interface UpdatePageLink {
  pageLinkId: string;
  sourcePageId: string;
  targetPageId: string;
  anchorText?: string;
}

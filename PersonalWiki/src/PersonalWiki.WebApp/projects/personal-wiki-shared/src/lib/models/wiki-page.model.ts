import { PageStatus } from './page-status.enum';

export interface WikiPage {
  wikiPageId: string;
  userId: string;
  categoryId?: string;
  title: string;
  slug: string;
  content: string;
  status: PageStatus;
  version: number;
  isFeatured: boolean;
  viewCount: number;
  lastModifiedAt: string;
  createdAt: string;
}

export interface CreateWikiPage {
  userId: string;
  categoryId?: string;
  title: string;
  slug: string;
  content: string;
  status: PageStatus;
  isFeatured: boolean;
}

export interface UpdateWikiPage {
  wikiPageId: string;
  categoryId?: string;
  title: string;
  slug: string;
  content: string;
  status: PageStatus;
  isFeatured: boolean;
}

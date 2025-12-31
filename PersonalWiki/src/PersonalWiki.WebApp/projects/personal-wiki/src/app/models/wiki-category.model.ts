export interface WikiCategory {
  wikiCategoryId: string;
  userId: string;
  name: string;
  description?: string;
  parentCategoryId?: string;
  icon?: string;
  createdAt: string;
  pageCount: number;
}

export interface CreateWikiCategory {
  userId: string;
  name: string;
  description?: string;
  parentCategoryId?: string;
  icon?: string;
}

export interface UpdateWikiCategory {
  wikiCategoryId: string;
  name: string;
  description?: string;
  parentCategoryId?: string;
  icon?: string;
}

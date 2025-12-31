export interface DocumentCategory {
  documentCategoryId: string;
  name: string;
  description?: string;
  createdAt: string;
}

export interface CreateDocumentCategoryCommand {
  name: string;
  description?: string;
}

export interface UpdateDocumentCategoryCommand {
  documentCategoryId: string;
  name: string;
  description?: string;
}

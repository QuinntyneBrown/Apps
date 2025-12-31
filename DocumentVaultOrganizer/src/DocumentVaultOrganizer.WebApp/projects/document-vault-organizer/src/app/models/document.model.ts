import { DocumentCategoryEnum } from './document-category.enum';

export interface Document {
  documentId: string;
  userId: string;
  name: string;
  category: DocumentCategoryEnum;
  fileUrl?: string;
  expirationDate?: string;
  createdAt: string;
}

export interface CreateDocumentCommand {
  userId: string;
  name: string;
  category: DocumentCategoryEnum;
  fileUrl?: string;
  expirationDate?: string;
}

export interface UpdateDocumentCommand {
  documentId: string;
  userId: string;
  name: string;
  category: DocumentCategoryEnum;
  fileUrl?: string;
  expirationDate?: string;
}

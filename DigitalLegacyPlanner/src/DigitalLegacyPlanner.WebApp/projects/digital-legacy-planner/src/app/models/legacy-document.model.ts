export interface LegacyDocument {
  legacyDocumentId: string;
  userId: string;
  title: string;
  documentType: string;
  filePath?: string;
  description?: string;
  physicalLocation?: string;
  accessGrantedTo?: string;
  isEncrypted: boolean;
  lastReviewedAt?: Date;
  createdAt: Date;
}

export interface CreateLegacyDocumentCommand {
  userId: string;
  title: string;
  documentType: string;
  filePath?: string;
  description?: string;
  physicalLocation?: string;
  accessGrantedTo?: string;
  isEncrypted: boolean;
}

export interface UpdateLegacyDocumentCommand {
  legacyDocumentId: string;
  title: string;
  documentType: string;
  filePath?: string;
  description?: string;
  physicalLocation?: string;
  accessGrantedTo?: string;
  isEncrypted: boolean;
}

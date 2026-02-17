import { ResourceType } from './resource-type.enum';

export interface Resource {
  resourceId: string;
  userId: string;
  title: string;
  resourceType: ResourceType;
  author?: string;
  publisher?: string;
  publicationDate?: string;
  url?: string;
  isbn?: string;
  totalPages?: number;
  topics: string[];
  dateAdded: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateResourceCommand {
  userId: string;
  title: string;
  resourceType: ResourceType;
  author?: string;
  publisher?: string;
  publicationDate?: string;
  url?: string;
  isbn?: string;
  totalPages?: number;
  topics: string[];
  notes?: string;
}

export interface UpdateResourceCommand {
  resourceId: string;
  title: string;
  resourceType: ResourceType;
  author?: string;
  publisher?: string;
  publicationDate?: string;
  url?: string;
  isbn?: string;
  totalPages?: number;
  topics: string[];
  notes?: string;
}

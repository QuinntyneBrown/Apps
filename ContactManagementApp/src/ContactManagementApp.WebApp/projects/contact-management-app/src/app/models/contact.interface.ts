import { ContactType } from './contact-type.enum';

export interface Contact {
  contactId: string;
  userId: string;
  firstName: string;
  lastName: string;
  fullName: string;
  contactType: ContactType;
  company?: string;
  jobTitle?: string;
  email?: string;
  phone?: string;
  linkedInUrl?: string;
  location?: string;
  notes?: string;
  tags: string[];
  dateMet: string;
  lastContactedDate?: string;
  isPriority: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateContactRequest {
  userId: string;
  firstName: string;
  lastName: string;
  contactType: ContactType;
  company?: string;
  jobTitle?: string;
  email?: string;
  phone?: string;
  linkedInUrl?: string;
  location?: string;
  notes?: string;
  tags: string[];
  dateMet: string;
  isPriority: boolean;
}

export interface UpdateContactRequest {
  contactId: string;
  firstName: string;
  lastName: string;
  contactType: ContactType;
  company?: string;
  jobTitle?: string;
  email?: string;
  phone?: string;
  linkedInUrl?: string;
  location?: string;
  notes?: string;
  tags: string[];
  dateMet: string;
  isPriority: boolean;
}

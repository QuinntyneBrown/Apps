export interface Contact {
  contactId: string;
  userId: string;
  eventId: string;
  name: string;
  company?: string;
  jobTitle?: string;
  email?: string;
  linkedInUrl?: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateContactCommand {
  eventId: string;
  name: string;
  company?: string;
  jobTitle?: string;
  email?: string;
  linkedInUrl?: string;
  notes?: string;
}

export interface UpdateContactCommand {
  contactId: string;
  eventId: string;
  name: string;
  company?: string;
  jobTitle?: string;
  email?: string;
  linkedInUrl?: string;
  notes?: string;
}

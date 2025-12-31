export interface Certification {
  certificationId: string;
  userId: string;
  name: string;
  issuingOrganization: string;
  issueDate: string;
  expirationDate?: string;
  credentialId?: string;
  credentialUrl?: string;
  isActive: boolean;
  skillIds: string[];
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

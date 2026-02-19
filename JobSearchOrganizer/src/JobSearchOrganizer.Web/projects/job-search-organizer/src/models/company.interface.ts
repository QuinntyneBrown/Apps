export interface Company {
  companyId: string;
  userId: string;
  name: string;
  industry?: string;
  website?: string;
  location?: string;
  companySize?: string;
  cultureNotes?: string;
  researchNotes?: string;
  isTargetCompany: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateCompany {
  userId: string;
  name: string;
  industry?: string;
  website?: string;
  location?: string;
  companySize?: string;
  cultureNotes?: string;
  researchNotes?: string;
  isTargetCompany: boolean;
}

export interface UpdateCompany {
  companyId: string;
  name: string;
  industry?: string;
  website?: string;
  location?: string;
  companySize?: string;
  cultureNotes?: string;
  researchNotes?: string;
  isTargetCompany: boolean;
}

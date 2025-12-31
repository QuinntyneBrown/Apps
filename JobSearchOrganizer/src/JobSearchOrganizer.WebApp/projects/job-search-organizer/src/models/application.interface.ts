import { ApplicationStatus } from './application-status.enum';

export interface Application {
  applicationId: string;
  userId: string;
  companyId: string;
  jobTitle: string;
  jobUrl?: string;
  status: ApplicationStatus;
  appliedDate: string;
  salaryRange?: string;
  location?: string;
  jobType?: string;
  isRemote: boolean;
  contactPerson?: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateApplication {
  userId: string;
  companyId: string;
  jobTitle: string;
  jobUrl?: string;
  status: ApplicationStatus;
  appliedDate: string;
  salaryRange?: string;
  location?: string;
  jobType?: string;
  isRemote: boolean;
  contactPerson?: string;
  notes?: string;
}

export interface UpdateApplication {
  applicationId: string;
  jobTitle: string;
  jobUrl?: string;
  status: ApplicationStatus;
  appliedDate: string;
  salaryRange?: string;
  location?: string;
  jobType?: string;
  isRemote: boolean;
  contactPerson?: string;
  notes?: string;
}

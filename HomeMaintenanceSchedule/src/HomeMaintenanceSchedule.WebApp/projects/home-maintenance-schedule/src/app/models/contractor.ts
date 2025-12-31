export interface Contractor {
  contractorId: string;
  userId: string;
  name: string;
  specialty?: string;
  phoneNumber?: string;
  email?: string;
  website?: string;
  address?: string;
  licenseNumber?: string;
  isInsured: boolean;
  rating?: number;
  notes?: string;
  isActive: boolean;
  createdAt: string;
}

export interface CreateContractor {
  userId: string;
  name: string;
  specialty?: string;
  phoneNumber?: string;
  email?: string;
  website?: string;
  address?: string;
  licenseNumber?: string;
  isInsured: boolean;
  rating?: number;
  notes?: string;
}

export interface UpdateContractor {
  contractorId: string;
  name: string;
  specialty?: string;
  phoneNumber?: string;
  email?: string;
  website?: string;
  address?: string;
  licenseNumber?: string;
  isInsured: boolean;
  rating?: number;
  notes?: string;
  isActive: boolean;
}

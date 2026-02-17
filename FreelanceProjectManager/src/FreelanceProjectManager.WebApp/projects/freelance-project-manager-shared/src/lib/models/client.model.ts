export interface Client {
  clientId: string;
  userId: string;
  name: string;
  companyName?: string;
  email?: string;
  phone?: string;
  address?: string;
  website?: string;
  notes?: string;
  isActive: boolean;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateClientRequest {
  userId: string;
  name: string;
  companyName?: string;
  email?: string;
  phone?: string;
  address?: string;
  website?: string;
  notes?: string;
}

export interface UpdateClientRequest {
  clientId: string;
  userId: string;
  name: string;
  companyName?: string;
  email?: string;
  phone?: string;
  address?: string;
  website?: string;
  notes?: string;
  isActive: boolean;
}

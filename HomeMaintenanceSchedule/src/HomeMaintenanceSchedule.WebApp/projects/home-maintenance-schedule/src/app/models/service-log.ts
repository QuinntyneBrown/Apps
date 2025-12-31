export interface ServiceLog {
  serviceLogId: string;
  maintenanceTaskId: string;
  serviceDate: string;
  description: string;
  contractorId?: string;
  cost?: number;
  notes?: string;
  partsUsed?: string;
  laborHours?: number;
  warrantyExpiresAt?: string;
  createdAt: string;
}

export interface CreateServiceLog {
  maintenanceTaskId: string;
  serviceDate: string;
  description: string;
  contractorId?: string;
  cost?: number;
  notes?: string;
  partsUsed?: string;
  laborHours?: number;
  warrantyExpiresAt?: string;
}

export interface UpdateServiceLog {
  serviceLogId: string;
  maintenanceTaskId: string;
  serviceDate: string;
  description: string;
  contractorId?: string;
  cost?: number;
  notes?: string;
  partsUsed?: string;
  laborHours?: number;
  warrantyExpiresAt?: string;
}

export interface ServiceRecord {
  serviceRecordId: string;
  applianceId: string;
  serviceDate: string;
  serviceProvider?: string;
  description?: string;
  cost?: number;
  createdAt: string;
}

export interface CreateServiceRecordRequest {
  applianceId: string;
  serviceDate: string;
  serviceProvider?: string;
  description?: string;
  cost?: number;
}

export interface UpdateServiceRecordRequest {
  serviceRecordId: string;
  applianceId: string;
  serviceDate: string;
  serviceProvider?: string;
  description?: string;
  cost?: number;
}

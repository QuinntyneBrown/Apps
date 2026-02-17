export interface Warranty {
  warrantyId: string;
  applianceId: string;
  provider?: string;
  startDate?: string;
  endDate?: string;
  coverageDetails?: string;
  documentUrl?: string;
  createdAt: string;
}

export interface CreateWarrantyRequest {
  applianceId: string;
  provider?: string;
  startDate?: string;
  endDate?: string;
  coverageDetails?: string;
  documentUrl?: string;
}

export interface UpdateWarrantyRequest {
  warrantyId: string;
  applianceId: string;
  provider?: string;
  startDate?: string;
  endDate?: string;
  coverageDetails?: string;
  documentUrl?: string;
}

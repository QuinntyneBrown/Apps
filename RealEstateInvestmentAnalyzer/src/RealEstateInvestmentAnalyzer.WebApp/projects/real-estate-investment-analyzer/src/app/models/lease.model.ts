export interface Lease {
  leaseId: string;
  propertyId: string;
  tenantName: string;
  monthlyRent: number;
  startDate: string;
  endDate: string;
  securityDeposit: number;
  isActive: boolean;
  notes?: string;
}

export interface CreateLease {
  propertyId: string;
  tenantName: string;
  monthlyRent: number;
  startDate: string;
  endDate: string;
  securityDeposit: number;
  notes?: string;
}

export interface UpdateLease {
  leaseId: string;
  propertyId: string;
  tenantName: string;
  monthlyRent: number;
  startDate: string;
  endDate: string;
  securityDeposit: number;
  isActive: boolean;
  notes?: string;
}

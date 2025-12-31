import { WarrantyStatus } from './warranty-status.enum';
import { WarrantyType } from './warranty-type.enum';

export interface Warranty {
  warrantyId: string;
  purchaseId: string;
  warrantyType: WarrantyType;
  provider: string;
  startDate: string;
  endDate: string;
  durationMonths: number;
  status: WarrantyStatus;
  coverageDetails?: string;
  terms?: string;
  registrationNumber?: string;
  notes?: string;
  claimFiledDate?: string;
}

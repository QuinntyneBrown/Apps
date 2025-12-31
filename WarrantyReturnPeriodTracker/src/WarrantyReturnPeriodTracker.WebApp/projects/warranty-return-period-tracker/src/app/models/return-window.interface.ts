import { ReturnWindowStatus } from './return-window-status.enum';

export interface ReturnWindow {
  returnWindowId: string;
  purchaseId: string;
  startDate: string;
  endDate: string;
  durationDays: number;
  status: ReturnWindowStatus;
  policyDetails?: string;
  conditions?: string;
  restockingFeePercent?: number;
  notes?: string;
}

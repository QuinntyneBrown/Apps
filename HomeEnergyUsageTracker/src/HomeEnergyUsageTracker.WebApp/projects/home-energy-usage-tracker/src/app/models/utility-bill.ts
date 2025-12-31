import { UtilityType } from './utility-type';

export interface UtilityBill {
  utilityBillId: string;
  userId: string;
  utilityType: UtilityType;
  billingDate: Date;
  amount: number;
  usageAmount?: number;
  unit?: string;
  createdAt: Date;
}

export interface CreateUtilityBillRequest {
  userId: string;
  utilityType: UtilityType;
  billingDate: Date;
  amount: number;
  usageAmount?: number;
  unit?: string;
}

export interface UpdateUtilityBillRequest {
  utilityBillId: string;
  userId: string;
  utilityType: UtilityType;
  billingDate: Date;
  amount: number;
  usageAmount?: number;
  unit?: string;
}

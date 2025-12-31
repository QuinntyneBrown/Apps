export interface Usage {
  usageId: string;
  utilityBillId: string;
  date: Date;
  amount: number;
  createdAt: Date;
}

export interface CreateUsageRequest {
  utilityBillId: string;
  date: Date;
  amount: number;
}

export interface UpdateUsageRequest {
  usageId: string;
  utilityBillId: string;
  date: Date;
  amount: number;
}

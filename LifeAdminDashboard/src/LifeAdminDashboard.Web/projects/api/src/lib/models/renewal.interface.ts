export interface Renewal {
  renewalId: string;
  userId: string;
  name: string;
  renewalType: string;
  provider?: string;
  renewalDate: string;
  cost?: number;
  frequency: string;
  isAutoRenewal: boolean;
  isActive: boolean;
  notes?: string;
  createdAt: string;
}

export interface CreateRenewal {
  userId: string;
  name: string;
  renewalType: string;
  provider?: string;
  renewalDate: string;
  cost?: number;
  frequency: string;
  isAutoRenewal: boolean;
  notes?: string;
}

export interface UpdateRenewal {
  renewalId: string;
  name: string;
  renewalType: string;
  provider?: string;
  renewalDate: string;
  cost?: number;
  frequency: string;
  isAutoRenewal: boolean;
  isActive: boolean;
  notes?: string;
}

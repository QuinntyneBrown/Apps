export interface Plan {
  planId: string;
  name: string;
  state: string;
  accountNumber?: string;
  currentBalance: number;
  openedDate: string;
  administrator?: string;
  isActive: boolean;
  notes?: string;
}

export interface CreatePlan {
  name: string;
  state: string;
  accountNumber?: string;
  currentBalance: number;
  openedDate: string;
  administrator?: string;
  notes?: string;
}

export interface UpdatePlan {
  name: string;
  state: string;
  accountNumber?: string;
  currentBalance: number;
  administrator?: string;
  isActive: boolean;
  notes?: string;
}

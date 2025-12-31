export interface Bankroll {
  bankrollId: string;
  userId: string;
  amount: number;
  recordedDate: string;
  notes?: string;
  createdAt: string;
}

export interface CreateBankroll {
  userId: string;
  amount: number;
  recordedDate: string;
  notes?: string;
}

export interface UpdateBankroll {
  bankrollId: string;
  userId: string;
  amount: number;
  recordedDate: string;
  notes?: string;
}

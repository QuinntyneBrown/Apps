export interface SavingsTip {
  savingsTipId: string;
  title: string;
  description?: string;
  createdAt: Date;
}

export interface CreateSavingsTipRequest {
  title: string;
  description?: string;
}

export interface UpdateSavingsTipRequest {
  savingsTipId: string;
  title: string;
  description?: string;
}

export interface Receipt {
  receiptId: string;
  deductionId: string;
  fileName: string;
  fileUrl: string;
  uploadDate: string;
  notes?: string;
}

export interface CreateReceipt {
  deductionId: string;
  fileName: string;
  fileUrl: string;
  notes?: string;
}

export interface UpdateReceipt {
  receiptId: string;
  deductionId: string;
  fileName: string;
  fileUrl: string;
  notes?: string;
}

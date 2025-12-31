export interface TimeEntry {
  timeEntryId: string;
  userId: string;
  projectId: string;
  workDate: Date;
  hours: number;
  description: string;
  isBillable: boolean;
  isInvoiced: boolean;
  invoiceId?: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateTimeEntryRequest {
  userId: string;
  projectId: string;
  workDate: Date;
  hours: number;
  description: string;
  isBillable?: boolean;
}

export interface UpdateTimeEntryRequest {
  timeEntryId: string;
  userId: string;
  projectId: string;
  workDate: Date;
  hours: number;
  description: string;
  isBillable: boolean;
  isInvoiced: boolean;
  invoiceId?: string;
}

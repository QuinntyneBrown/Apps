export interface Invoice {
  invoiceId: string;
  userId: string;
  clientId: string;
  projectId?: string;
  invoiceNumber: string;
  invoiceDate: Date;
  dueDate: Date;
  totalAmount: number;
  currency: string;
  status: string;
  paidDate?: Date;
  notes?: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateInvoiceRequest {
  userId: string;
  clientId: string;
  projectId?: string;
  invoiceNumber: string;
  invoiceDate: Date;
  dueDate: Date;
  totalAmount: number;
  currency?: string;
  status?: string;
  notes?: string;
}

export interface UpdateInvoiceRequest {
  invoiceId: string;
  userId: string;
  clientId: string;
  projectId?: string;
  invoiceNumber: string;
  invoiceDate: Date;
  dueDate: Date;
  totalAmount: number;
  currency: string;
  status: string;
  paidDate?: Date;
  notes?: string;
}

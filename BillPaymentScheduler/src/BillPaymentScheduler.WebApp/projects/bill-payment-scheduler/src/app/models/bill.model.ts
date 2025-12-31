import { BillingFrequency } from './billing-frequency.enum';
import { BillStatus } from './bill-status.enum';

export interface Bill {
  billId: string;
  payeeId: string;
  name: string;
  amount: number;
  dueDate: string;
  billingFrequency: BillingFrequency;
  status: BillStatus;
  isAutoPay: boolean;
  notes?: string;
  payeeName?: string;
}

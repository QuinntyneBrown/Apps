import { PaymentMethod } from './payment-method.enum';
import { ReceiptFormat } from './receipt-format.enum';
import { ReceiptStatus } from './receipt-status.enum';
import { ReceiptType } from './receipt-type.enum';

export interface Receipt {
  receiptId: string;
  purchaseId: string;
  receiptNumber: string;
  receiptType: ReceiptType;
  format: ReceiptFormat;
  storageLocation?: string;
  receiptDate: string;
  storeName: string;
  totalAmount: number;
  paymentMethod: PaymentMethod;
  status: ReceiptStatus;
  isVerified: boolean;
  notes?: string;
  uploadedAt: string;
}

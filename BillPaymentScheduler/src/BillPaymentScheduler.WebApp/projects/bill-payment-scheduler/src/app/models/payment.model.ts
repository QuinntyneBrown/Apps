export interface Payment {
  paymentId: string;
  billId: string;
  amount: number;
  paymentDate: string;
  confirmationNumber?: string;
  paymentMethod?: string;
  notes?: string;
  billName?: string;
}

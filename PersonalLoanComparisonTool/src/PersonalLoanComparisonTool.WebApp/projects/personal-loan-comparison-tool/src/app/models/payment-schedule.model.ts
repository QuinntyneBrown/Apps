export interface PaymentSchedule {
  paymentScheduleId: string;
  offerId: string;
  paymentNumber: number;
  dueDate: string;
  paymentAmount: number;
  principalAmount: number;
  interestAmount: number;
  remainingBalance: number;
}

export interface CreatePaymentScheduleCommand {
  offerId: string;
  paymentNumber: number;
  dueDate: string;
  paymentAmount: number;
  principalAmount: number;
  interestAmount: number;
  remainingBalance: number;
}

export interface UpdatePaymentScheduleCommand {
  paymentScheduleId: string;
  offerId: string;
  paymentNumber: number;
  dueDate: string;
  paymentAmount: number;
  principalAmount: number;
  interestAmount: number;
  remainingBalance: number;
}

export interface Reminder {
  reminderId: string;
  userId: string;
  screeningId: string;
  reminderDate: string;
  message?: string;
  isSent: boolean;
  createdAt: string;
}

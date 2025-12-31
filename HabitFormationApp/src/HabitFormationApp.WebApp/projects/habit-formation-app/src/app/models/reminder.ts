export interface Reminder {
  reminderId: string;
  userId: string;
  habitId: string;
  reminderTime: string;
  message?: string;
  isEnabled: boolean;
  createdAt: string;
}

export interface CreateReminderRequest {
  userId: string;
  habitId: string;
  reminderTime: string;
  message?: string;
  isEnabled: boolean;
}

export interface UpdateReminderRequest {
  reminderId: string;
  reminderTime: string;
  message?: string;
  isEnabled: boolean;
}

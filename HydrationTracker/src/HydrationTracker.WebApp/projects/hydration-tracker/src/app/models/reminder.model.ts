export interface Reminder {
  reminderId: string;
  userId: string;
  reminderTime: string; // TimeSpan as string in format "HH:mm:ss"
  message?: string;
  isEnabled: boolean;
  createdAt: Date;
}

export interface CreateReminderCommand {
  userId: string;
  reminderTime: string; // TimeSpan as string in format "HH:mm:ss"
  message?: string;
  isEnabled: boolean;
}

export interface UpdateReminderCommand {
  reminderId: string;
  userId: string;
  reminderTime: string; // TimeSpan as string in format "HH:mm:ss"
  message?: string;
  isEnabled: boolean;
}

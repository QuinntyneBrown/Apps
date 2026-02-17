import { NotificationChannel } from './enums';

export interface EventReminder {
  reminderId: string;
  eventId: string;
  recipientId: string;
  reminderTime: string;
  deliveryChannel: NotificationChannel;
  sent: boolean;
}

export interface CreateReminderRequest {
  eventId: string;
  recipientId: string;
  reminderTime: string;
  deliveryChannel: NotificationChannel;
}

export interface RescheduleReminderRequest {
  reminderId: string;
  reminderTime: string;
}

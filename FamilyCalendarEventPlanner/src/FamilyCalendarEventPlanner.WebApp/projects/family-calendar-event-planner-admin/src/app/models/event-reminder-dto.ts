export interface EventReminderDto {
  reminderId: string;
  eventId: string;
  recipientId: string;
  reminderTime: string;
  deliveryChannel: string;
  sent: boolean;
}

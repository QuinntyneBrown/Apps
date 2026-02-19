export interface CreateReminderCommand {
  eventId: string;
  recipientId: string;
  reminderTime: string;
  deliveryChannel: string;
}

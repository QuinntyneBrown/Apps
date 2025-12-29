import { DeliveryChannel } from './delivery-channel';
import { ReminderStatus } from './reminder-status';

export interface Reminder {
  reminderId: string;
  dateId: string;
  scheduledTime: Date;
  advanceNoticeDays: number;
  deliveryChannel: DeliveryChannel;
  status: ReminderStatus;
  sentAt: Date | null;
}

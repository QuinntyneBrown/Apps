import { DeliveryChannel } from './delivery-channel';

export interface ReminderSettings {
  oneWeekBefore: boolean;
  threeDaysBefore: boolean;
  oneDayBefore: boolean;
  channels: DeliveryChannel[];
}

import { DeliveryStatus } from './delivery-status.enum';

export interface Letter {
  letterId: string;
  userId: string;
  subject: string;
  content: string;
  writtenDate: string;
  scheduledDeliveryDate: string;
  actualDeliveryDate?: string;
  deliveryStatus: DeliveryStatus;
  hasBeenRead: boolean;
  readDate?: string;
  createdAt: string;
  updatedAt?: string;
  isDueForDelivery: boolean;
}

export interface CreateLetter {
  userId: string;
  subject: string;
  content: string;
  scheduledDeliveryDate: string;
}

export interface UpdateLetter {
  letterId: string;
  subject: string;
  content: string;
  scheduledDeliveryDate: string;
}

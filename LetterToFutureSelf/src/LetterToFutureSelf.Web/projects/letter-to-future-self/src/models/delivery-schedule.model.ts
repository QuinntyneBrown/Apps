export interface DeliverySchedule {
  deliveryScheduleId: string;
  letterId: string;
  scheduledDateTime: string;
  deliveryMethod: string;
  recipientContact?: string;
  isActive: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateDeliverySchedule {
  letterId: string;
  scheduledDateTime: string;
  deliveryMethod: string;
  recipientContact?: string;
}

export interface UpdateDeliverySchedule {
  deliveryScheduleId: string;
  scheduledDateTime: string;
  deliveryMethod: string;
  recipientContact?: string;
}

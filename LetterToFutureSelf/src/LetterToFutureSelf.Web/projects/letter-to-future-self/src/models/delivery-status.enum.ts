export enum DeliveryStatus {
  Pending = 0,
  Delivered = 1,
  Cancelled = 2,
  Failed = 3
}

export const DeliveryStatusLabels: Record<DeliveryStatus, string> = {
  [DeliveryStatus.Pending]: 'Pending',
  [DeliveryStatus.Delivered]: 'Delivered',
  [DeliveryStatus.Cancelled]: 'Cancelled',
  [DeliveryStatus.Failed]: 'Failed'
};

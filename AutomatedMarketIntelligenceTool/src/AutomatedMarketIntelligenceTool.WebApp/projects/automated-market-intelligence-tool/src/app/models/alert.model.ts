export enum AlertType {
  CompetitorActivity = 0,
  MarketThreshold = 1,
  Keyword = 2
}

export const AlertTypeLabels: Record<AlertType, string> = {
  [AlertType.CompetitorActivity]: 'Competitor Activity',
  [AlertType.MarketThreshold]: 'Market Threshold',
  [AlertType.Keyword]: 'Keyword'
};

export enum NotificationPreference {
  Email = 0,
  InApp = 1,
  Both = 2
}

export const NotificationPreferenceLabels: Record<NotificationPreference, string> = {
  [NotificationPreference.Email]: 'Email',
  [NotificationPreference.InApp]: 'In-App',
  [NotificationPreference.Both]: 'Both'
};

export interface Alert {
  alertId: string;
  tenantId: string;
  name: string;
  description?: string;
  alertType: AlertType;
  isActive: boolean;
  criteria?: string;
  notificationPreference: NotificationPreference;
  lastTriggered?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateAlertRequest {
  name: string;
  description?: string;
  alertType: AlertType;
  isActive: boolean;
  criteria?: string;
  notificationPreference: NotificationPreference;
}

export interface UpdateAlertRequest {
  alertId: string;
  name: string;
  description?: string;
  alertType: AlertType;
  isActive: boolean;
  criteria?: string;
  notificationPreference: NotificationPreference;
}

export interface ExpirationAlert {
  expirationAlertId: string;
  documentId: string;
  alertDate: string;
  isAcknowledged: boolean;
  createdAt: string;
}

export interface CreateExpirationAlertCommand {
  documentId: string;
  alertDate: string;
}

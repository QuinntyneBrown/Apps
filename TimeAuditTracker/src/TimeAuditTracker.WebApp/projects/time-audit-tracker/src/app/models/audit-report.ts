export interface AuditReport {
  auditReportId: string;
  userId: string;
  title: string;
  startDate: Date;
  endDate: Date;
  totalTrackedHours: number;
  productiveHours: number;
  summary?: string;
  insights?: string;
  recommendations?: string;
  createdAt: Date;
  productivityPercentage: number;
  periodDays: number;
}

export interface CreateAuditReportRequest {
  userId: string;
  title: string;
  startDate: Date;
  endDate: Date;
  totalTrackedHours: number;
  productiveHours: number;
  summary?: string;
  insights?: string;
  recommendations?: string;
}

export interface UpdateAuditReportRequest {
  title: string;
  startDate: Date;
  endDate: Date;
  totalTrackedHours: number;
  productiveHours: number;
  summary?: string;
  insights?: string;
  recommendations?: string;
}

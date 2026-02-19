import { SessionType } from './session-type.enum';

export interface SessionAnalytics {
  sessionAnalyticsId: string;
  userId: string;
  periodStartDate: string;
  periodEndDate: string;
  totalSessions: number;
  totalFocusMinutes: number;
  averageFocusScore?: number | null;
  totalDistractions: number;
  completionRate: number;
  mostProductiveSessionType?: SessionType | null;
  averageSessionDuration: number;
  averageDistractions: number;
  createdAt: string;
}

export interface GenerateAnalyticsCommand {
  userId: string;
  periodStartDate: string;
  periodEndDate: string;
}

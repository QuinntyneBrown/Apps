export interface HealthTrend {
  healthTrendId: string;
  userId: string;
  metricName: string;
  startDate: string;
  endDate: string;
  averageValue: number;
  minValue: number;
  maxValue: number;
  trendDirection: string;
  percentageChange: number;
  insights: string | null;
  createdAt: string;
  periodDuration: number;
}

export interface CreateHealthTrend {
  userId: string;
  metricName: string;
  startDate: string;
  endDate: string;
  averageValue: number;
  minValue: number;
  maxValue: number;
  trendDirection: string;
  percentageChange: number;
  insights: string | null;
}

export interface UpdateHealthTrend {
  healthTrendId: string;
  metricName: string;
  startDate: string;
  endDate: string;
  averageValue: number;
  minValue: number;
  maxValue: number;
  trendDirection: string;
  percentageChange: number;
  insights: string | null;
}

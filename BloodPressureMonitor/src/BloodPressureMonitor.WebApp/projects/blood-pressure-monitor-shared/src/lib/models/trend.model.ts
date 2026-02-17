export interface Trend {
  trendId: string;
  userId: string;
  startDate: string;
  endDate: string;
  averageSystolic: number;
  averageDiastolic: number;
  highestSystolic: number;
  highestDiastolic: number;
  lowestSystolic: number;
  lowestDiastolic: number;
  readingCount: number;
  trendDirection: string;
  insights?: string;
  createdAt: string;
  periodDuration: number;
  isImproving: boolean;
}

export interface CalculateTrendRequest {
  userId: string;
  startDate: string;
  endDate: string;
}

export interface Pattern {
  patternId: string;
  userId: string;
  name: string;
  description: string;
  patternType: string;
  startDate: string;
  endDate: string;
  confidenceLevel: number;
  insights?: string;
  createdAt: string;
  isHighConfidence: boolean;
  durationDays: number;
}

export interface CreatePatternRequest {
  userId: string;
  name: string;
  description: string;
  patternType: string;
  startDate: string;
  endDate: string;
  confidenceLevel: number;
  insights?: string;
}

export interface UpdatePatternRequest {
  patternId: string;
  name: string;
  description: string;
  patternType: string;
  startDate: string;
  endDate: string;
  confidenceLevel: number;
  insights?: string;
}

export enum MarketPosition {
  Leader = 0,
  Challenger = 1,
  Follower = 2,
  Nicher = 3
}

export const MarketPositionLabels: Record<MarketPosition, string> = {
  [MarketPosition.Leader]: 'Leader',
  [MarketPosition.Challenger]: 'Challenger',
  [MarketPosition.Follower]: 'Follower',
  [MarketPosition.Nicher]: 'Nicher'
};

export interface Competitor {
  competitorId: string;
  tenantId: string;
  name: string;
  industry?: string;
  website?: string;
  description?: string;
  employeeCount?: number;
  annualRevenue?: number;
  marketPosition: MarketPosition;
  strengths?: string;
  weaknesses?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateCompetitorRequest {
  name: string;
  industry?: string;
  website?: string;
  description?: string;
  employeeCount?: number;
  annualRevenue?: number;
  marketPosition: MarketPosition;
  strengths?: string;
  weaknesses?: string;
}

export interface UpdateCompetitorRequest {
  competitorId: string;
  name: string;
  industry?: string;
  website?: string;
  description?: string;
  employeeCount?: number;
  annualRevenue?: number;
  marketPosition: MarketPosition;
  strengths?: string;
  weaknesses?: string;
}

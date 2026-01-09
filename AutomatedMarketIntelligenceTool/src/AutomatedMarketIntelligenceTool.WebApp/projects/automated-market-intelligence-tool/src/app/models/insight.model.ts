export enum InsightCategory {
  Trend = 0,
  Opportunity = 1,
  Threat = 2,
  General = 3
}

export const InsightCategoryLabels: Record<InsightCategory, string> = {
  [InsightCategory.Trend]: 'Trend',
  [InsightCategory.Opportunity]: 'Opportunity',
  [InsightCategory.Threat]: 'Threat',
  [InsightCategory.General]: 'General'
};

export enum InsightImpact {
  High = 0,
  Medium = 1,
  Low = 2
}

export const InsightImpactLabels: Record<InsightImpact, string> = {
  [InsightImpact.High]: 'High',
  [InsightImpact.Medium]: 'Medium',
  [InsightImpact.Low]: 'Low'
};

export interface Insight {
  insightId: string;
  tenantId: string;
  title: string;
  description: string;
  category: InsightCategory;
  impact: InsightImpact;
  source?: string;
  sourceUrl?: string;
  tags: string[];
  isActionable: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateInsightRequest {
  title: string;
  description: string;
  category: InsightCategory;
  impact: InsightImpact;
  source?: string;
  sourceUrl?: string;
  tags: string[];
  isActionable: boolean;
}

export interface UpdateInsightRequest {
  insightId: string;
  title: string;
  description: string;
  category: InsightCategory;
  impact: InsightImpact;
  source?: string;
  sourceUrl?: string;
  tags: string[];
  isActionable: boolean;
}

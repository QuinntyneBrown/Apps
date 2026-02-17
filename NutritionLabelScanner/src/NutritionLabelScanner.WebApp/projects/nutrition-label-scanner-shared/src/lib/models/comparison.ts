export interface Comparison {
  comparisonId: string;
  userId: string;
  name: string;
  productIds: string;
  results?: string;
  winnerProductId?: string;
  createdAt: string;
}

export interface CreateComparison {
  userId: string;
  name: string;
  productIds: string;
  results?: string;
  winnerProductId?: string;
}

export interface UpdateComparison {
  comparisonId: string;
  userId: string;
  name: string;
  productIds: string;
  results?: string;
  winnerProductId?: string;
}

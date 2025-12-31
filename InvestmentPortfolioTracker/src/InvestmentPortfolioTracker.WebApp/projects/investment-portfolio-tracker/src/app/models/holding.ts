export interface Holding {
  holdingId: string;
  accountId: string;
  symbol: string;
  name: string;
  shares: number;
  averageCost: number;
  currentPrice: number;
  lastPriceUpdate: string;
  marketValue: number;
  costBasis: number;
  unrealizedGainLoss: number;
}

export interface CreateHolding {
  accountId: string;
  symbol: string;
  name: string;
  shares: number;
  averageCost: number;
  currentPrice: number;
}

export interface UpdateHolding {
  holdingId: string;
  symbol: string;
  name: string;
  shares: number;
  averageCost: number;
  currentPrice: number;
}

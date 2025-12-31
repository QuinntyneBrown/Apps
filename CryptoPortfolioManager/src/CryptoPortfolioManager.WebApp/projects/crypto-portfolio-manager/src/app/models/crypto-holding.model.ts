export interface CryptoHolding {
  cryptoHoldingId: string;
  walletId: string;
  symbol: string;
  name: string;
  quantity: number;
  averageCost: number;
  currentPrice: number;
  lastPriceUpdate: Date;
  marketValue: number;
  unrealizedGainLoss: number;
}

export interface CreateCryptoHoldingRequest {
  walletId: string;
  symbol: string;
  name: string;
  quantity: number;
  averageCost: number;
  currentPrice: number;
}

export interface UpdateCryptoHoldingRequest {
  cryptoHoldingId: string;
  walletId: string;
  symbol: string;
  name: string;
  quantity: number;
  averageCost: number;
  currentPrice: number;
}

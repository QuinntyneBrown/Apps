export interface TaxLot {
  taxLotId: string;
  cryptoHoldingId: string;
  acquisitionDate: Date;
  quantity: number;
  costBasis: number;
  isDisposed: boolean;
  disposalDate?: Date;
  disposalPrice?: number;
  realizedGainLoss?: number;
}

export interface CreateTaxLotRequest {
  cryptoHoldingId: string;
  acquisitionDate: Date;
  quantity: number;
  costBasis: number;
}

export interface UpdateTaxLotRequest {
  taxLotId: string;
  cryptoHoldingId: string;
  acquisitionDate: Date;
  quantity: number;
  costBasis: number;
}

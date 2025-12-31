export interface MarketComparison {
  marketComparisonId: string;
  vehicleId: string;
  comparisonDate: string;
  listingSource: string;
  comparableYear: number;
  comparableMake: string;
  comparableModel: string;
  comparableTrim?: string;
  comparableMileage: number;
  askingPrice: number;
  location?: string;
  condition?: string;
  listingUrl?: string;
  daysOnMarket?: number;
  notes?: string;
  isActive: boolean;
}

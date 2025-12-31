export interface EfficiencyReport {
  efficiencyReportId: string;
  vehicleId: string;
  startDate: Date;
  endDate: Date;
  totalMiles: number;
  totalGallons: number;
  averageMPG: number;
  totalFuelCost: number;
  costPerMile: number;
  numberOfFillUps: number;
  bestMPG?: number;
  worstMPG?: number;
  notes?: string;
}

export interface GenerateEfficiencyReportRequest {
  vehicleId: string;
  startDate: Date;
  endDate: Date;
  notes?: string;
}

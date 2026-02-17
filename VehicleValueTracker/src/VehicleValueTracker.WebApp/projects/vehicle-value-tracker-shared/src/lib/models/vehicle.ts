export interface Vehicle {
  vehicleId: string;
  make: string;
  model: string;
  year: number;
  trim?: string;
  vin?: string;
  currentMileage: number;
  purchasePrice?: number;
  purchaseDate?: string;
  color?: string;
  interiorType?: string;
  engineType?: string;
  transmission?: string;
  isCurrentlyOwned: boolean;
  notes?: string;
}

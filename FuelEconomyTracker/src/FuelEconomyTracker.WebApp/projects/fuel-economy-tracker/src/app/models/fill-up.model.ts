export interface FillUp {
  fillUpId: string;
  vehicleId: string;
  fillUpDate: Date;
  odometer: number;
  gallons: number;
  pricePerGallon: number;
  totalCost: number;
  isFullTank: boolean;
  fuelGrade?: string;
  gasStation?: string;
  milesPerGallon?: number;
  notes?: string;
}

export interface CreateFillUpRequest {
  vehicleId: string;
  fillUpDate: Date;
  odometer: number;
  gallons: number;
  pricePerGallon: number;
  isFullTank: boolean;
  fuelGrade?: string;
  gasStation?: string;
  notes?: string;
}

export interface UpdateFillUpRequest {
  fillUpDate: Date;
  odometer: number;
  gallons: number;
  pricePerGallon: number;
  isFullTank: boolean;
  fuelGrade?: string;
  gasStation?: string;
  notes?: string;
}

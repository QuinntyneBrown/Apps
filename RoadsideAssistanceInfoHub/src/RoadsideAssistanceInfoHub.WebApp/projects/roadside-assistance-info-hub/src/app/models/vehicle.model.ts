export interface Vehicle {
  vehicleId: string;
  make: string;
  model: string;
  year: number;
  vin?: string;
  licensePlate?: string;
  color?: string;
  currentMileage?: number;
  ownerName?: string;
  notes?: string;
  isActive: boolean;
}

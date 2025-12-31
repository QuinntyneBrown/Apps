import { VehicleType } from './vehicle-type.enum';

export interface Vehicle {
  vehicleId: string;
  make: string;
  model: string;
  year: number;
  vin?: string;
  licensePlate?: string;
  vehicleType: VehicleType;
  currentMileage: number;
  purchaseDate?: string;
  notes?: string;
  isActive: boolean;
}

export interface CreateVehicleRequest {
  make: string;
  model: string;
  year: number;
  vin?: string;
  licensePlate?: string;
  vehicleType: VehicleType;
  currentMileage: number;
  purchaseDate?: string;
  notes?: string;
}

export interface UpdateVehicleRequest extends CreateVehicleRequest {
  vehicleId: string;
}

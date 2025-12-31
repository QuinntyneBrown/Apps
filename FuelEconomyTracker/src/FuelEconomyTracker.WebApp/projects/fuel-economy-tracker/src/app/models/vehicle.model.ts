export interface Vehicle {
  vehicleId: string;
  make: string;
  model: string;
  year: number;
  vin?: string;
  licensePlate?: string;
  tankCapacity?: number;
  epaCityMPG?: number;
  epaHighwayMPG?: number;
  isActive: boolean;
}

export interface CreateVehicleRequest {
  make: string;
  model: string;
  year: number;
  vin?: string;
  licensePlate?: string;
  tankCapacity?: number;
  epaCityMPG?: number;
  epaHighwayMPG?: number;
}

export interface UpdateVehicleRequest {
  make: string;
  model: string;
  year: number;
  vin?: string;
  licensePlate?: string;
  tankCapacity?: number;
  epaCityMPG?: number;
  epaHighwayMPG?: number;
  isActive: boolean;
}

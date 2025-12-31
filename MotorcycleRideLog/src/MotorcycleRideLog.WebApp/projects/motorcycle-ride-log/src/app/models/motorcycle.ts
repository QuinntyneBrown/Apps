import { MotorcycleType } from './motorcycle-type';

export interface Motorcycle {
  motorcycleId: string;
  userId: string;
  make: string;
  model: string;
  year?: number;
  type: MotorcycleType;
  vin?: string;
  licensePlate?: string;
  currentMileage?: number;
  color?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateMotorcycle {
  userId: string;
  make: string;
  model: string;
  year?: number;
  type: MotorcycleType;
  vin?: string;
  licensePlate?: string;
  currentMileage?: number;
  color?: string;
  notes?: string;
}

export interface UpdateMotorcycle {
  motorcycleId: string;
  make: string;
  model: string;
  year?: number;
  type: MotorcycleType;
  vin?: string;
  licensePlate?: string;
  currentMileage?: number;
  color?: string;
  notes?: string;
}

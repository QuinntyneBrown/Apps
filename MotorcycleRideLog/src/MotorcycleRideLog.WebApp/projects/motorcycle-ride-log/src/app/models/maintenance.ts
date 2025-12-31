import { MaintenanceType } from './maintenance-type';

export interface Maintenance {
  maintenanceId: string;
  userId: string;
  motorcycleId: string;
  maintenanceDate: string;
  type: MaintenanceType;
  mileageAtMaintenance?: number;
  description: string;
  cost?: number;
  serviceProvider?: string;
  partsReplaced?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateMaintenance {
  userId: string;
  motorcycleId: string;
  maintenanceDate: string;
  type: MaintenanceType;
  mileageAtMaintenance?: number;
  description: string;
  cost?: number;
  serviceProvider?: string;
  partsReplaced?: string;
  notes?: string;
}

export interface UpdateMaintenance {
  maintenanceId: string;
  motorcycleId: string;
  maintenanceDate: string;
  type: MaintenanceType;
  mileageAtMaintenance?: number;
  description: string;
  cost?: number;
  serviceProvider?: string;
  partsReplaced?: string;
  notes?: string;
}

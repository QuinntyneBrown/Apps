import { ServiceType } from './service-type.enum';

export interface MaintenanceSchedule {
  maintenanceScheduleId: string;
  vehicleId: string;
  serviceType: ServiceType;
  description: string;
  mileageInterval?: number;
  monthsInterval?: number;
  lastServiceMileage?: number;
  lastServiceDate?: string;
  nextServiceMileage?: number;
  nextServiceDate?: string;
  isActive: boolean;
  notes?: string;
}

export interface CreateMaintenanceScheduleRequest {
  vehicleId: string;
  serviceType: ServiceType;
  description: string;
  mileageInterval?: number;
  monthsInterval?: number;
  lastServiceMileage?: number;
  lastServiceDate?: string;
  notes?: string;
}

export interface UpdateMaintenanceScheduleRequest extends CreateMaintenanceScheduleRequest {
  maintenanceScheduleId: string;
  isActive: boolean;
}

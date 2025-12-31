import { ServiceType } from './service-type.enum';

export interface ServiceRecord {
  serviceRecordId: string;
  vehicleId: string;
  serviceType: ServiceType;
  serviceDate: string;
  mileageAtService: number;
  cost: number;
  serviceProvider?: string;
  description: string;
  notes?: string;
  partsReplaced: string[];
  invoiceNumber?: string;
  warrantyExpirationDate?: string;
}

export interface CreateServiceRecordRequest {
  vehicleId: string;
  serviceType: ServiceType;
  serviceDate: string;
  mileageAtService: number;
  cost: number;
  serviceProvider?: string;
  description: string;
  notes?: string;
  partsReplaced: string[];
  invoiceNumber?: string;
  warrantyExpirationDate?: string;
}

export interface UpdateServiceRecordRequest extends CreateServiceRecordRequest {
  serviceRecordId: string;
}

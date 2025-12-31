import { ApplianceType } from './appliance-type.enum';

export interface Appliance {
  applianceId: string;
  userId: string;
  name: string;
  applianceType: ApplianceType;
  brand?: string;
  modelNumber?: string;
  serialNumber?: string;
  purchaseDate?: string;
  purchasePrice?: number;
  createdAt: string;
}

export interface CreateApplianceRequest {
  userId: string;
  name: string;
  applianceType: ApplianceType;
  brand?: string;
  modelNumber?: string;
  serialNumber?: string;
  purchaseDate?: string;
  purchasePrice?: number;
}

export interface UpdateApplianceRequest {
  applianceId: string;
  userId: string;
  name: string;
  applianceType: ApplianceType;
  brand?: string;
  modelNumber?: string;
  serialNumber?: string;
  purchaseDate?: string;
  purchasePrice?: number;
}

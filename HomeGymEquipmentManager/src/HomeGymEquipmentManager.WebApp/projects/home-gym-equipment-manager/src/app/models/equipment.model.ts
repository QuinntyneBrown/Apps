import { EquipmentType } from './equipment-type.enum';

export interface Equipment {
  equipmentId?: string;
  userId: string;
  name: string;
  equipmentType: EquipmentType;
  brand?: string;
  model?: string;
  purchaseDate?: string;
  purchasePrice?: number;
  location?: string;
  notes?: string;
  isActive?: boolean;
  createdAt?: string;
}

import { ModCategory } from './mod-category.enum';

export interface Part {
  partId: string;
  name: string;
  partNumber?: string;
  manufacturer: string;
  description: string;
  price: number;
  category: ModCategory;
  compatibleVehicles: string[];
  warrantyInfo?: string;
  weight?: number;
  dimensions?: string;
  inStock: boolean;
  supplier?: string;
  notes?: string;
}

export interface CreatePartCommand {
  name: string;
  partNumber?: string;
  manufacturer: string;
  description: string;
  price: number;
  category: ModCategory;
  compatibleVehicles?: string[];
  warrantyInfo?: string;
  weight?: number;
  dimensions?: string;
  inStock: boolean;
  supplier?: string;
  notes?: string;
}

export interface UpdatePartCommand extends CreatePartCommand {
  partId: string;
}

import { PropertyType } from './property-type.enum';

export interface Property {
  propertyId: string;
  address: string;
  propertyType: PropertyType;
  purchasePrice: number;
  purchaseDate: string;
  currentValue: number;
  squareFeet: number;
  bedrooms: number;
  bathrooms: number;
  notes?: string;
  equity: number;
  roi: number;
}

export interface CreateProperty {
  address: string;
  propertyType: PropertyType;
  purchasePrice: number;
  purchaseDate: string;
  currentValue: number;
  squareFeet: number;
  bedrooms: number;
  bathrooms: number;
  notes?: string;
}

export interface UpdateProperty {
  propertyId: string;
  address: string;
  propertyType: PropertyType;
  purchasePrice: number;
  purchaseDate: string;
  currentValue: number;
  squareFeet: number;
  bedrooms: number;
  bathrooms: number;
  notes?: string;
}

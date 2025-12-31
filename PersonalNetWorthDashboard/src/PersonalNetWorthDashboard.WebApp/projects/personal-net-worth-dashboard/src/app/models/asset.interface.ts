import { AssetType } from './asset-type.enum';

export interface Asset {
  assetId: string;
  name: string;
  assetType: AssetType;
  currentValue: number;
  purchasePrice?: number;
  purchaseDate?: string;
  institution?: string;
  accountNumber?: string;
  notes?: string;
  lastUpdated: string;
  isActive: boolean;
}

export interface CreateAsset {
  name: string;
  assetType: AssetType;
  currentValue: number;
  purchasePrice?: number;
  purchaseDate?: string;
  institution?: string;
  accountNumber?: string;
  notes?: string;
}

export interface UpdateAsset {
  assetId: string;
  name: string;
  assetType: AssetType;
  currentValue: number;
  purchasePrice?: number;
  purchaseDate?: string;
  institution?: string;
  accountNumber?: string;
  notes?: string;
}

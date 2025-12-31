import { WineType } from './wine-type.enum';
import { Region } from './region.enum';

export interface Wine {
  wineId: string;
  userId: string;
  name: string;
  wineType: WineType;
  region: Region;
  vintage?: number;
  producer?: string;
  purchasePrice?: number;
  bottleCount: number;
  storageLocation?: string;
  notes?: string;
  createdAt: string;
}

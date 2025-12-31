import { Category } from './category.enum';
import { Room } from './room.enum';

export interface Item {
  itemId: string;
  userId: string;
  name: string;
  description?: string | null;
  category: Category;
  room: Room;
  brand?: string | null;
  modelNumber?: string | null;
  serialNumber?: string | null;
  purchaseDate?: string | null;
  purchasePrice?: number | null;
  currentValue?: number | null;
  quantity: number;
  photoUrl?: string | null;
  receiptUrl?: string | null;
  notes?: string | null;
  createdAt: string;
  updatedAt: string;
}

export interface CreateItemCommand {
  userId: string;
  name: string;
  description?: string | null;
  category: Category;
  room: Room;
  brand?: string | null;
  modelNumber?: string | null;
  serialNumber?: string | null;
  purchaseDate?: string | null;
  purchasePrice?: number | null;
  currentValue?: number | null;
  quantity: number;
  photoUrl?: string | null;
  receiptUrl?: string | null;
  notes?: string | null;
}

export interface UpdateItemCommand {
  itemId: string;
  name: string;
  description?: string | null;
  category: Category;
  room: Room;
  brand?: string | null;
  modelNumber?: string | null;
  serialNumber?: string | null;
  purchaseDate?: string | null;
  purchasePrice?: number | null;
  currentValue?: number | null;
  quantity: number;
  photoUrl?: string | null;
  receiptUrl?: string | null;
  notes?: string | null;
}

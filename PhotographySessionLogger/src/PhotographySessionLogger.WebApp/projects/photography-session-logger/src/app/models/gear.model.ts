export interface Gear {
  gearId: string;
  userId: string;
  name: string;
  gearType: string;
  brand?: string;
  model?: string;
  purchaseDate?: string;
  purchasePrice?: number;
  notes?: string;
  createdAt: string;
}

export interface CreateGear {
  userId: string;
  name: string;
  gearType: string;
  brand?: string;
  model?: string;
  purchaseDate?: string;
  purchasePrice?: number;
  notes?: string;
}

export interface UpdateGear {
  gearId: string;
  name: string;
  gearType: string;
  brand?: string;
  model?: string;
  purchaseDate?: string;
  purchasePrice?: number;
  notes?: string;
}

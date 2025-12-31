export interface GearChecklist {
  gearChecklistId: string;
  userId: string;
  tripId: string;
  itemName: string;
  isPacked: boolean;
  quantity: number;
  notes?: string;
  createdAt: Date;
}

export interface CreateGearChecklist {
  userId: string;
  tripId: string;
  itemName: string;
  quantity: number;
  notes?: string;
}

export interface UpdateGearChecklist {
  gearChecklistId: string;
  userId: string;
  tripId: string;
  itemName: string;
  isPacked: boolean;
  quantity: number;
  notes?: string;
}

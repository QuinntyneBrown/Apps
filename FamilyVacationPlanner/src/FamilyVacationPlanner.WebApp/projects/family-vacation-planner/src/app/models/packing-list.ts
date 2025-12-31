export interface PackingList {
  packingListId: string;
  tripId: string;
  itemName: string;
  isPacked: boolean;
  createdAt: string;
}

export interface CreatePackingListCommand {
  tripId: string;
  itemName: string;
  isPacked: boolean;
}

export interface UpdatePackingListCommand {
  packingListId: string;
  tripId: string;
  itemName: string;
  isPacked: boolean;
}

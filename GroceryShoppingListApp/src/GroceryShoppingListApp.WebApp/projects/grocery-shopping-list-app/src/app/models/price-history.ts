export interface PriceHistory {
  priceHistoryId: string;
  groceryItemId: string;
  storeId: string;
  price: number;
  date: Date;
  createdAt: Date;
}

export interface CreatePriceHistoryRequest {
  groceryItemId: string;
  storeId: string;
  price: number;
  date: Date;
}

export interface UpdatePriceHistoryRequest {
  priceHistoryId: string;
  groceryItemId: string;
  storeId: string;
  price: number;
  date: Date;
}

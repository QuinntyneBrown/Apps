export interface Purchase {
  purchaseId: string;
  giftIdeaId: string;
  purchaseDate: string;
  actualPrice: number;
  store: string | null;
  createdAt: string;
}

export interface CreatePurchaseRequest {
  giftIdeaId: string;
  purchaseDate: string;
  actualPrice: number;
  store?: string;
}

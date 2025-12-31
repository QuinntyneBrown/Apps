export interface Store {
  storeId: string;
  userId: string;
  name: string;
  address: string | null;
  createdAt: Date;
}

export interface CreateStoreRequest {
  userId: string;
  name: string;
  address?: string;
}

export interface UpdateStoreRequest {
  storeId: string;
  name: string;
  address?: string;
}

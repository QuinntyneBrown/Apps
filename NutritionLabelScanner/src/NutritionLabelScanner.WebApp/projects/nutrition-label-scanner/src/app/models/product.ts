export interface Product {
  productId: string;
  userId: string;
  name: string;
  brand?: string;
  barcode?: string;
  category?: string;
  servingSize?: string;
  scannedAt: string;
  notes?: string;
  createdAt: string;
}

export interface CreateProduct {
  userId: string;
  name: string;
  brand?: string;
  barcode?: string;
  category?: string;
  servingSize?: string;
  scannedAt: string;
  notes?: string;
}

export interface UpdateProduct {
  productId: string;
  userId: string;
  name: string;
  brand?: string;
  barcode?: string;
  category?: string;
  servingSize?: string;
  scannedAt: string;
  notes?: string;
}

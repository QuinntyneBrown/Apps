import { ProductCategory } from './product-category.enum';
import { PurchaseStatus } from './purchase-status.enum';

export interface Purchase {
  purchaseId: string;
  userId: string;
  productName: string;
  category: ProductCategory;
  storeName: string;
  purchaseDate: string;
  price: number;
  status: PurchaseStatus;
  modelNumber?: string;
  notes?: string;
  createdAt: string;
}

export interface Category {
  categoryId: string;
  name: string;
  description?: string;
  maxAmount?: number;
  requiresReceipt: boolean;
  isActive: boolean;
}

export interface Category {
  categoryId: string;
  name: string;
  description?: string;
  colorCode?: string;
  totalMonthlyCost: number;
  subscriptionCount: number;
}

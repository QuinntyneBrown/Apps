export interface GroceryList {
  groceryListId: string;
  userId: string;
  mealPlanId?: string;
  name: string;
  items: string;
  shoppingDate?: Date;
  isCompleted: boolean;
  estimatedCost?: number;
  notes?: string;
  createdAt: Date;
  isScheduledForToday: boolean;
}

export interface GroceryItem {
  name: string;
  quantity: number;
  unit: string;
  category: string;
  isPurchased: boolean;
  notes?: string;
}

export interface CreateGroceryListCommand {
  userId: string;
  mealPlanId?: string;
  name: string;
  items: string;
  shoppingDate?: Date;
  estimatedCost?: number;
  notes?: string;
}

export interface UpdateGroceryListCommand {
  groceryListId: string;
  userId: string;
  mealPlanId?: string;
  name: string;
  items: string;
  shoppingDate?: Date;
  isCompleted: boolean;
  estimatedCost?: number;
  notes?: string;
}

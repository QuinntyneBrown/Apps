export interface ShoppingList {
  shoppingListId: string;
  userId: string;
  name: string;
  items: string;
  createdDate: string;
  isCompleted: boolean;
  completedDate?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateShoppingListRequest {
  userId: string;
  name: string;
  items: string;
  notes?: string;
}

export interface UpdateShoppingListRequest {
  shoppingListId: string;
  name: string;
  items: string;
  isCompleted: boolean;
  notes?: string;
}

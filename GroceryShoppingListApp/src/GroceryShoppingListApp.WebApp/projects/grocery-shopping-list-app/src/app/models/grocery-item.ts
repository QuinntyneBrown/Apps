import { Category } from './category';

export interface GroceryItem {
  groceryItemId: string;
  groceryListId: string | null;
  name: string;
  category: Category;
  quantity: number;
  isChecked: boolean;
  createdAt: Date;
}

export interface CreateGroceryItemRequest {
  groceryListId: string | null;
  name: string;
  category: Category;
  quantity: number;
}

export interface UpdateGroceryItemRequest {
  groceryItemId: string;
  groceryListId: string | null;
  name: string;
  category: Category;
  quantity: number;
  isChecked: boolean;
}

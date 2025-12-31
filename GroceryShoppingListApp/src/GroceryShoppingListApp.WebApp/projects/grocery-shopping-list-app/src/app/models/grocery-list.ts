export interface GroceryList {
  groceryListId: string;
  userId: string;
  name: string;
  createdDate: Date;
  isCompleted: boolean;
  createdAt: Date;
}

export interface CreateGroceryListRequest {
  userId: string;
  name: string;
}

export interface UpdateGroceryListRequest {
  groceryListId: string;
  name: string;
  isCompleted: boolean;
}

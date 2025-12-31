export interface Ingredient {
  ingredientId: string;
  recipeId: string;
  name: string;
  quantity: string;
  unit?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateIngredientRequest {
  recipeId: string;
  name: string;
  quantity: string;
  unit?: string;
  notes?: string;
}

export interface UpdateIngredientRequest {
  ingredientId: string;
  name: string;
  quantity: string;
  unit?: string;
  notes?: string;
}

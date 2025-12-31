export interface Recipe {
  recipeId: string;
  userId: string;
  mealPlanId?: string;
  name: string;
  description?: string;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  servings: number;
  ingredients: string;
  instructions: string;
  mealType: string;
  tags?: string;
  isFavorite: boolean;
  createdAt: Date;
  totalTime: number;
  isQuickMeal: boolean;
}

export interface CreateRecipeCommand {
  userId: string;
  mealPlanId?: string;
  name: string;
  description?: string;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  servings: number;
  ingredients: string;
  instructions: string;
  mealType: string;
  tags?: string;
  isFavorite: boolean;
}

export interface UpdateRecipeCommand {
  recipeId: string;
  userId: string;
  mealPlanId?: string;
  name: string;
  description?: string;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  servings: number;
  ingredients: string;
  instructions: string;
  mealType: string;
  tags?: string;
  isFavorite: boolean;
}

import { MeatType, CookingMethod } from './enums';

export interface Recipe {
  recipeId: string;
  userId: string;
  name: string;
  description: string;
  meatType: MeatType;
  cookingMethod: CookingMethod;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  ingredients: string;
  instructions: string;
  targetTemperature: number | null;
  servings: number;
  notes: string | null;
  isFavorite: boolean;
  createdAt: string;
}

export interface CreateRecipe {
  userId: string;
  name: string;
  description: string;
  meatType: MeatType;
  cookingMethod: CookingMethod;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  ingredients: string;
  instructions: string;
  targetTemperature: number | null;
  servings: number;
  notes: string | null;
}

export interface UpdateRecipe {
  recipeId: string;
  name: string;
  description: string;
  meatType: MeatType;
  cookingMethod: CookingMethod;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  ingredients: string;
  instructions: string;
  targetTemperature: number | null;
  servings: number;
  notes: string | null;
}

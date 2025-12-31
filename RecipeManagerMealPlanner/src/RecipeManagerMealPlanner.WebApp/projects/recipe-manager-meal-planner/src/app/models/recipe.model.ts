import { Cuisine } from './cuisine.enum';
import { DifficultyLevel } from './difficulty-level.enum';

export interface Recipe {
  recipeId: string;
  userId: string;
  name: string;
  description?: string;
  cuisine: Cuisine;
  difficultyLevel: DifficultyLevel;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  servings: number;
  instructions: string;
  photoUrl?: string;
  source?: string;
  rating?: number;
  notes?: string;
  isFavorite: boolean;
  createdAt: string;
}

export interface CreateRecipeRequest {
  userId: string;
  name: string;
  description?: string;
  cuisine: Cuisine;
  difficultyLevel: DifficultyLevel;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  servings: number;
  instructions: string;
  photoUrl?: string;
  source?: string;
  notes?: string;
}

export interface UpdateRecipeRequest {
  recipeId: string;
  name: string;
  description?: string;
  cuisine: Cuisine;
  difficultyLevel: DifficultyLevel;
  prepTimeMinutes: number;
  cookTimeMinutes: number;
  servings: number;
  instructions: string;
  photoUrl?: string;
  source?: string;
  rating?: number;
  notes?: string;
  isFavorite: boolean;
}

import { BeerStyle } from './beer-style.enum';

export interface Recipe {
  recipeId: string;
  userId: string;
  name: string;
  beerStyle: BeerStyle;
  description: string;
  originalGravity?: number;
  finalGravity?: number;
  abv?: number;
  ibu?: number;
  batchSize: number;
  ingredients: string;
  instructions: string;
  notes?: string;
  isFavorite: boolean;
  createdAt: Date;
}

export interface CreateRecipeRequest {
  userId: string;
  name: string;
  beerStyle: BeerStyle;
  description: string;
  originalGravity?: number;
  finalGravity?: number;
  abv?: number;
  ibu?: number;
  batchSize: number;
  ingredients: string;
  instructions: string;
  notes?: string;
}

export interface UpdateRecipeRequest {
  recipeId: string;
  name: string;
  beerStyle: BeerStyle;
  description: string;
  originalGravity?: number;
  finalGravity?: number;
  abv?: number;
  ibu?: number;
  batchSize: number;
  ingredients: string;
  instructions: string;
  notes?: string;
}

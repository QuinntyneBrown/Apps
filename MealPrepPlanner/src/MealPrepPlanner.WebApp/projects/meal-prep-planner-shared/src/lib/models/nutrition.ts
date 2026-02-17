export interface Nutrition {
  nutritionId: string;
  userId: string;
  recipeId?: string;
  calories: number;
  protein: number;
  carbohydrates: number;
  fat: number;
  fiber?: number;
  sugar?: number;
  sodium?: number;
  additionalNutrients?: string;
  createdAt: Date;
  proteinCaloriesPercentage: number;
  isHighProtein: boolean;
}

export interface CreateNutritionCommand {
  userId: string;
  recipeId?: string;
  calories: number;
  protein: number;
  carbohydrates: number;
  fat: number;
  fiber?: number;
  sugar?: number;
  sodium?: number;
  additionalNutrients?: string;
}

export interface UpdateNutritionCommand {
  nutritionId: string;
  userId: string;
  recipeId?: string;
  calories: number;
  protein: number;
  carbohydrates: number;
  fat: number;
  fiber?: number;
  sugar?: number;
  sodium?: number;
  additionalNutrients?: string;
}

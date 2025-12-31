export interface MealPlan {
  mealPlanId: string;
  userId: string;
  name: string;
  mealDate: string;
  mealType: string;
  recipeId?: string;
  notes?: string;
  isPrepared: boolean;
  createdAt: string;
}

export interface CreateMealPlanRequest {
  userId: string;
  name: string;
  mealDate: string;
  mealType: string;
  recipeId?: string;
  notes?: string;
}

export interface UpdateMealPlanRequest {
  mealPlanId: string;
  name: string;
  mealDate: string;
  mealType: string;
  recipeId?: string;
  notes?: string;
  isPrepared: boolean;
}

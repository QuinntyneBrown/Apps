export interface MealPlan {
  mealPlanId: string;
  userId: string;
  name: string;
  startDate: Date;
  endDate: Date;
  dailyCalorieTarget?: number;
  dietaryPreferences?: string;
  isActive: boolean;
  notes?: string;
  createdAt: Date;
  duration: number;
  isCurrentlyActive: boolean;
}

export interface CreateMealPlanCommand {
  userId: string;
  name: string;
  startDate: Date;
  endDate: Date;
  dailyCalorieTarget?: number;
  dietaryPreferences?: string;
  notes?: string;
}

export interface UpdateMealPlanCommand {
  mealPlanId: string;
  userId: string;
  name: string;
  startDate: Date;
  endDate: Date;
  dailyCalorieTarget?: number;
  dietaryPreferences?: string;
  isActive: boolean;
  notes?: string;
}

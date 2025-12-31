export interface NutritionInfo {
  nutritionInfoId: string;
  productId: string;
  calories: number;
  totalFat: number;
  saturatedFat?: number;
  transFat?: number;
  cholesterol?: number;
  sodium: number;
  totalCarbohydrates: number;
  dietaryFiber?: number;
  totalSugars?: number;
  protein: number;
  additionalNutrients?: string;
  createdAt: string;
}

export interface CreateNutritionInfo {
  productId: string;
  calories: number;
  totalFat: number;
  saturatedFat?: number;
  transFat?: number;
  cholesterol?: number;
  sodium: number;
  totalCarbohydrates: number;
  dietaryFiber?: number;
  totalSugars?: number;
  protein: number;
  additionalNutrients?: string;
}

export interface UpdateNutritionInfo {
  nutritionInfoId: string;
  productId: string;
  calories: number;
  totalFat: number;
  saturatedFat?: number;
  transFat?: number;
  cholesterol?: number;
  sodium: number;
  totalCarbohydrates: number;
  dietaryFiber?: number;
  totalSugars?: number;
  protein: number;
  additionalNutrients?: string;
}

export interface CookSession {
  cookSessionId: string;
  userId: string;
  recipeId: string;
  cookDate: string;
  actualCookTimeMinutes: number;
  temperatureUsed: number | null;
  rating: number | null;
  notes: string | null;
  modifications: string | null;
  wasSuccessful: boolean;
  createdAt: string;
}

export interface CreateCookSession {
  userId: string;
  recipeId: string;
  cookDate: string;
  actualCookTimeMinutes: number;
  temperatureUsed: number | null;
  rating: number | null;
  notes: string | null;
  modifications: string | null;
  wasSuccessful: boolean;
}

export interface UpdateCookSession {
  cookSessionId: string;
  cookDate: string;
  actualCookTimeMinutes: number;
  temperatureUsed: number | null;
  rating: number | null;
  notes: string | null;
  modifications: string | null;
  wasSuccessful: boolean;
}

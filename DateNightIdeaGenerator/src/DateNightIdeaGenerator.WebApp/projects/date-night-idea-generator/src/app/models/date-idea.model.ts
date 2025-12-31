import { Category } from './category.enum';
import { BudgetRange } from './budget-range.enum';

export interface DateIdea {
  dateIdeaId: string;
  userId: string;
  title: string;
  description: string;
  category: Category;
  budgetRange: BudgetRange;
  location?: string;
  durationMinutes?: number;
  season?: string;
  isFavorite: boolean;
  hasBeenTried: boolean;
  createdAt: Date;
  updatedAt?: Date;
  averageRating?: number;
}

export interface CreateDateIdea {
  userId: string;
  title: string;
  description: string;
  category: Category;
  budgetRange: BudgetRange;
  location?: string;
  durationMinutes?: number;
  season?: string;
}

export interface UpdateDateIdea {
  dateIdeaId: string;
  title: string;
  description: string;
  category: Category;
  budgetRange: BudgetRange;
  location?: string;
  durationMinutes?: number;
  season?: string;
  isFavorite: boolean;
}

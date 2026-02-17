import { Routes } from '@angular/router';
import { Dashboard } from '@lib/dashboard';
import { Recipes } from '@lib/recipes';
import { MealPlans } from '@lib/meal-plans';
import { Grocery } from '@lib/grocery';
import { Nutrition } from '@lib/nutrition';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'recipes', component: Recipes },
  { path: 'meal-plans', component: MealPlans },
  { path: 'grocery', component: Grocery },
  { path: 'nutrition', component: Nutrition },
  { path: '**', redirectTo: '' }
];

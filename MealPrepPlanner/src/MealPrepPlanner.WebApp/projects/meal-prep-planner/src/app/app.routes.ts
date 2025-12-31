import { Routes } from '@angular/router';
import { Dashboard, Recipes, MealPlans, Grocery, Nutrition } from './pages';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'recipes', component: Recipes },
  { path: 'meal-plans', component: MealPlans },
  { path: 'grocery', component: Grocery },
  { path: 'nutrition', component: Nutrition },
  { path: '**', redirectTo: '' }
];

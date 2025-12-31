import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard';
import { Recipes } from './pages/recipes/recipes';
import { MealPlans } from './pages/meal-plans/meal-plans';
import { ShoppingLists } from './pages/shopping-lists/shopping-lists';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'recipes', component: Recipes },
  { path: 'meal-plans', component: MealPlans },
  { path: 'shopping-lists', component: ShoppingLists },
];

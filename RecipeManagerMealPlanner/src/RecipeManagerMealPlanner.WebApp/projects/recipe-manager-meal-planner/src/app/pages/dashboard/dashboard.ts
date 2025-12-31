import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RecipeService, MealPlanService, ShoppingListService } from '../../services';
import { Recipe, MealPlan, ShoppingList } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss'],
})
export class Dashboard implements OnInit {
  recipes$ = this.recipeService.recipes$;
  mealPlans$ = this.mealPlanService.mealPlans$;
  shoppingLists$ = this.shoppingListService.shoppingLists$;
  loading$ = this.recipeService.loading$;

  constructor(
    private recipeService: RecipeService,
    private mealPlanService: MealPlanService,
    private shoppingListService: ShoppingListService
  ) {}

  ngOnInit(): void {
    const userId = '00000000-0000-0000-0000-000000000001';
    this.recipeService.getRecipes(userId).subscribe();
    this.mealPlanService.getMealPlans(userId).subscribe();
    this.shoppingListService.getShoppingLists(userId).subscribe();
  }
}

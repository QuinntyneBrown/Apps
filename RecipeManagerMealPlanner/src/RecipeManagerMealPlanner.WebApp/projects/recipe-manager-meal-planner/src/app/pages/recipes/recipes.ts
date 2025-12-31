import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { RecipeService } from '../../services';
import { RecipeCard } from '../../components/recipe-card/recipe-card';
import { RecipeDialog } from '../../components/recipe-dialog/recipe-dialog';
import { Recipe, CreateRecipeRequest, UpdateRecipeRequest, Cuisine, DifficultyLevel } from '../../models';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatFormFieldModule,
    RecipeCard,
  ],
  templateUrl: './recipes.html',
  styleUrls: ['./recipes.scss'],
})
export class Recipes implements OnInit {
  recipes$ = this.recipeService.recipes$;
  loading$ = this.recipeService.loading$;
  userId = '00000000-0000-0000-0000-000000000001';

  cuisineFilter = new FormControl<Cuisine | null>(null);
  difficultyFilter = new FormControl<DifficultyLevel | null>(null);
  favoritesOnlyFilter = new FormControl<boolean>(false);

  cuisines = Object.keys(Cuisine).filter((k) => !isNaN(Number(k))).map((k) => ({
    value: Number(k) as Cuisine,
    label: Cuisine[Number(k)],
  }));

  difficultyLevels = Object.keys(DifficultyLevel).filter((k) => !isNaN(Number(k))).map((k) => ({
    value: Number(k) as DifficultyLevel,
    label: DifficultyLevel[Number(k)],
  }));

  constructor(
    private recipeService: RecipeService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadRecipes();
    this.setupFilters();
  }

  private setupFilters(): void {
    this.cuisineFilter.valueChanges.subscribe(() => this.loadRecipes());
    this.difficultyFilter.valueChanges.subscribe(() => this.loadRecipes());
    this.favoritesOnlyFilter.valueChanges.subscribe(() => this.loadRecipes());
  }

  loadRecipes(): void {
    this.recipeService.getRecipes(
      this.userId,
      this.cuisineFilter.value ?? undefined,
      this.difficultyFilter.value ?? undefined,
      this.favoritesOnlyFilter.value ?? undefined
    ).subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(RecipeDialog, {
      width: '600px',
      data: { userId: this.userId },
    });

    dialogRef.afterClosed().subscribe((result: CreateRecipeRequest) => {
      if (result) {
        this.recipeService.createRecipe(result).subscribe();
      }
    });
  }

  onEdit(recipe: Recipe): void {
    const dialogRef = this.dialog.open(RecipeDialog, {
      width: '600px',
      data: { recipe, userId: this.userId },
    });

    dialogRef.afterClosed().subscribe((result: UpdateRecipeRequest) => {
      if (result) {
        this.recipeService.updateRecipe(result).subscribe();
      }
    });
  }

  onDelete(recipeId: string): void {
    if (confirm('Are you sure you want to delete this recipe?')) {
      this.recipeService.deleteRecipe(recipeId).subscribe();
    }
  }

  onToggleFavorite(recipe: Recipe): void {
    const request: UpdateRecipeRequest = {
      ...recipe,
      isFavorite: !recipe.isFavorite,
    };
    this.recipeService.updateRecipe(request).subscribe();
  }

  clearFilters(): void {
    this.cuisineFilter.setValue(null);
    this.difficultyFilter.setValue(null);
    this.favoritesOnlyFilter.setValue(false);
  }
}

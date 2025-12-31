import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { RecipeService } from '../services';
import { Recipe, BeerStyleLabels } from '../models';

@Component({
  selector: 'app-recipes-list',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  template: `
    <div class="recipes-list">
      <div class="recipes-list__header">
        <h1 class="recipes-list__title">Recipes</h1>
        <button mat-raised-button color="primary" (click)="createRecipe()">
          <mat-icon>add</mat-icon>
          New Recipe
        </button>
      </div>

      <div class="recipes-list__grid" *ngIf="(recipeService.recipes$ | async) as recipes">
        <mat-card class="recipes-list__card" *ngFor="let recipe of recipes" (click)="viewRecipe(recipe.recipeId)">
          <mat-card-header>
            <mat-card-title>{{ recipe.name }}</mat-card-title>
            <mat-card-subtitle>{{ getBeerStyleLabel(recipe.beerStyle) }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <p class="recipes-list__description">{{ recipe.description }}</p>
            <div class="recipes-list__stats">
              <span *ngIf="recipe.abv"><strong>ABV:</strong> {{ recipe.abv }}%</span>
              <span *ngIf="recipe.ibu"><strong>IBU:</strong> {{ recipe.ibu }}</span>
              <span><strong>Batch Size:</strong> {{ recipe.batchSize }}L</span>
            </div>
            <mat-chip-set *ngIf="recipe.isFavorite">
              <mat-chip class="recipes-list__favorite">
                <mat-icon>star</mat-icon>
                Favorite
              </mat-chip>
            </mat-chip-set>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="viewRecipe(recipe.recipeId); $event.stopPropagation()">
              <mat-icon>visibility</mat-icon>
              View
            </button>
            <button mat-button color="accent" (click)="editRecipe(recipe.recipeId); $event.stopPropagation()">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="deleteRecipe(recipe.recipeId); $event.stopPropagation()">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>

        <div *ngIf="recipes.length === 0" class="recipes-list__empty">
          <mat-icon class="recipes-list__empty-icon">book</mat-icon>
          <h2>No recipes yet</h2>
          <p>Create your first brewing recipe to get started!</p>
          <button mat-raised-button color="primary" (click)="createRecipe()">
            <mat-icon>add</mat-icon>
            Create Recipe
          </button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .recipes-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        font-size: 2rem;
        margin: 0;
      }

      &__grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        cursor: pointer;
        transition: transform 0.2s, box-shadow 0.2s;

        &:hover {
          transform: translateY(-4px);
          box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
        }
      }

      &__description {
        margin: 1rem 0;
        color: #666;
        min-height: 3rem;
        overflow: hidden;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }

      &__stats {
        display: flex;
        gap: 1rem;
        margin-bottom: 1rem;
        flex-wrap: wrap;

        span {
          font-size: 0.875rem;
        }
      }

      &__favorite {
        background-color: #ffd700 !important;

        mat-icon {
          font-size: 1rem;
          height: 1rem;
          width: 1rem;
        }
      }

      &__empty {
        grid-column: 1 / -1;
        text-align: center;
        padding: 4rem 2rem;
        color: #666;

        &-icon {
          font-size: 4rem;
          height: 4rem;
          width: 4rem;
          color: #ccc;
        }

        h2 {
          margin: 1rem 0;
        }

        p {
          margin-bottom: 2rem;
        }
      }
    }
  `]
})
export class RecipesList implements OnInit {
  constructor(
    public recipeService: RecipeService,
    private router: Router
  ) {}

  ngOnInit() {
    this.recipeService.getRecipes().subscribe();
  }

  getBeerStyleLabel(style: number): string {
    return BeerStyleLabels[style] || 'Unknown';
  }

  createRecipe() {
    this.router.navigate(['/recipes/new']);
  }

  viewRecipe(id: string) {
    this.router.navigate(['/recipes', id]);
  }

  editRecipe(id: string) {
    this.router.navigate(['/recipes', id, 'edit']);
  }

  deleteRecipe(id: string) {
    if (confirm('Are you sure you want to delete this recipe?')) {
      this.recipeService.deleteRecipe(id).subscribe();
    }
  }
}

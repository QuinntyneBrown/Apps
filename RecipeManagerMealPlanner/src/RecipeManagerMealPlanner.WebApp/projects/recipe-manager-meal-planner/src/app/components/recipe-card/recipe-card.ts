import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Recipe, Cuisine, DifficultyLevel } from '../../models';

@Component({
  selector: 'app-recipe-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
  ],
  templateUrl: './recipe-card.html',
  styleUrls: ['./recipe-card.scss'],
})
export class RecipeCard {
  @Input() recipe!: Recipe;
  @Output() edit = new EventEmitter<Recipe>();
  @Output() delete = new EventEmitter<string>();
  @Output() toggleFavorite = new EventEmitter<Recipe>();

  readonly Cuisine = Cuisine;
  readonly DifficultyLevel = DifficultyLevel;

  getCuisineName(cuisine: Cuisine): string {
    return Cuisine[cuisine];
  }

  getDifficultyName(level: DifficultyLevel): string {
    return DifficultyLevel[level];
  }

  getTotalTime(): number {
    return this.recipe.prepTimeMinutes + this.recipe.cookTimeMinutes;
  }

  onEdit(): void {
    this.edit.emit(this.recipe);
  }

  onDelete(): void {
    this.delete.emit(this.recipe.recipeId);
  }

  onToggleFavorite(): void {
    this.toggleFavorite.emit(this.recipe);
  }
}

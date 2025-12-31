import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { PromptService, FavoriteService } from '../../services';
import { Prompt, Category, Depth, CategoryLabels, DepthLabels } from '../../models';

@Component({
  selector: 'app-prompts',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule
  ],
  templateUrl: './prompts.html',
  styleUrl: './prompts.scss'
})
export class Prompts implements OnInit {
  prompts$: Observable<Prompt[]>;
  categoryLabels = CategoryLabels;
  depthLabels = DepthLabels;
  categories = Object.values(Category).filter(v => typeof v === 'number') as Category[];
  depths = Object.values(Depth).filter(v => typeof v === 'number') as Depth[];

  selectedCategory?: Category;
  selectedDepth?: Depth;

  // For demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private promptService: PromptService,
    private favoriteService: FavoriteService,
    private snackBar: MatSnackBar
  ) {
    this.prompts$ = this.promptService.prompts$;
  }

  ngOnInit(): void {
    this.loadPrompts();
    this.loadFavorites();
  }

  loadPrompts(): void {
    this.promptService.getAll(undefined, this.selectedCategory, this.selectedDepth).subscribe();
  }

  loadFavorites(): void {
    this.favoriteService.getByUserId(this.userId).subscribe();
  }

  applyFilters(): void {
    this.loadPrompts();
  }

  clearFilters(): void {
    this.selectedCategory = undefined;
    this.selectedDepth = undefined;
    this.loadPrompts();
  }

  usePrompt(prompt: Prompt): void {
    this.promptService.incrementUsage(prompt.promptId).subscribe({
      next: () => {
        this.snackBar.open('Prompt marked as used!', 'Close', { duration: 2000 });
      }
    });
  }

  toggleFavorite(prompt: Prompt): void {
    const favorite = this.favoriteService.getFavoriteByPromptId(prompt.promptId);

    if (favorite) {
      this.favoriteService.delete(favorite.favoriteId).subscribe({
        next: () => {
          this.snackBar.open('Removed from favorites', 'Close', { duration: 2000 });
        }
      });
    } else {
      this.favoriteService.create({
        userId: this.userId,
        promptId: prompt.promptId
      }).subscribe({
        next: () => {
          this.snackBar.open('Added to favorites!', 'Close', { duration: 2000 });
        }
      });
    }
  }

  isFavorite(promptId: string): boolean {
    return this.favoriteService.isFavorite(promptId);
  }

  getRandom(): void {
    this.promptService.getRandom(this.selectedCategory, this.selectedDepth).subscribe({
      next: (prompt) => {
        this.snackBar.open('Random prompt selected!', 'Close', { duration: 2000 });
      }
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { FavoriteService, PromptService } from '../../services';
import { Favorite, CategoryLabels, DepthLabels } from '../../models';

@Component({
  selector: 'app-favorites',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatSnackBarModule
  ],
  templateUrl: './favorites.html',
  styleUrl: './favorites.scss'
})
export class Favorites implements OnInit {
  favorites$: Observable<Favorite[]>;
  categoryLabels = CategoryLabels;
  depthLabels = DepthLabels;

  // For demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private favoriteService: FavoriteService,
    private promptService: PromptService,
    private snackBar: MatSnackBar
  ) {
    this.favorites$ = this.favoriteService.favorites$;
  }

  ngOnInit(): void {
    this.loadFavorites();
  }

  loadFavorites(): void {
    this.favoriteService.getByUserId(this.userId).subscribe();
  }

  removeFavorite(favorite: Favorite): void {
    this.favoriteService.delete(favorite.favoriteId).subscribe({
      next: () => {
        this.snackBar.open('Removed from favorites', 'Close', { duration: 2000 });
      }
    });
  }

  usePrompt(favorite: Favorite): void {
    if (favorite.prompt) {
      this.promptService.incrementUsage(favorite.prompt.promptId).subscribe({
        next: () => {
          this.snackBar.open('Prompt marked as used!', 'Close', { duration: 2000 });
        }
      });
    }
  }
}

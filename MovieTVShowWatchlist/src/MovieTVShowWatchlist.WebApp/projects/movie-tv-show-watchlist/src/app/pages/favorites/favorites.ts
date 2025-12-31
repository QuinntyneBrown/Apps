import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { FavoriteService } from '../../services';

@Component({
  selector: 'app-favorites',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './favorites.html',
  styleUrl: './favorites.scss'
})
export class Favorites implements OnInit {
  private readonly favoriteService = inject(FavoriteService);

  favorites$ = this.favoriteService.favorites$;
  displayedColumns = ['contentType', 'addedDate', 'favoriteCategory', 'rewatchCount', 'emotionalSignificance', 'actions'];

  ngOnInit(): void {
    this.favoriteService.getAll().subscribe();
  }

  deleteFavorite(id: string): void {
    if (confirm('Are you sure you want to remove this favorite?')) {
      this.favoriteService.delete(id).subscribe();
    }
  }
}

import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { RatingService } from '../../services';

@Component({
  selector: 'app-ratings',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './ratings.html',
  styleUrl: './ratings.scss'
})
export class Ratings implements OnInit {
  private readonly ratingService = inject(RatingService);

  ratings$ = this.ratingService.ratings$;
  displayedColumns = ['contentType', 'ratingValue', 'ratingScale', 'ratingDate', 'viewingDate', 'isRewatchRating', 'mood', 'actions'];

  ngOnInit(): void {
    this.ratingService.getAll().subscribe();
  }

  deleteRating(id: string): void {
    if (confirm('Are you sure you want to delete this rating?')) {
      this.ratingService.delete(id).subscribe();
    }
  }
}

import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { WeeklyReview } from '../../models';

@Component({
  selector: 'app-review-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './review-card.html',
  styleUrl: './review-card.scss'
})
export class ReviewCard {
  @Input() review!: WeeklyReview;
  @Output() edit = new EventEmitter<WeeklyReview>();
  @Output() delete = new EventEmitter<string>();
  @Output() view = new EventEmitter<WeeklyReview>();

  onEdit(): void {
    this.edit.emit(this.review);
  }

  onDelete(): void {
    this.delete.emit(this.review.weeklyReviewId);
  }

  onView(): void {
    this.view.emit(this.review);
  }
}

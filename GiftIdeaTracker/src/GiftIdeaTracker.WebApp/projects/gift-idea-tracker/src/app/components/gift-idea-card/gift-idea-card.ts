import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { GiftIdea, Occasion } from '../../models';

@Component({
  selector: 'app-gift-idea-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './gift-idea-card.html',
  styleUrl: './gift-idea-card.scss'
})
export class GiftIdeaCard {
  @Input() giftIdea!: GiftIdea;
  @Output() purchaseClick = new EventEmitter<GiftIdea>();
  @Output() editClick = new EventEmitter<GiftIdea>();
  @Output() deleteClick = new EventEmitter<GiftIdea>();

  onPurchaseClick(): void {
    this.purchaseClick.emit(this.giftIdea);
  }

  onEditClick(): void {
    this.editClick.emit(this.giftIdea);
  }

  onDeleteClick(): void {
    this.deleteClick.emit(this.giftIdea);
  }

  getOccasionIcon(): string {
    const icons: Record<string, string> = {
      'Birthday': 'cake',
      'Anniversary': 'favorite',
      'Christmas': 'park',
      'Graduation': 'school',
      'Wedding': 'favorite_border',
      'Other': 'card_giftcard'
    };
    return icons[this.giftIdea.occasion] || 'card_giftcard';
  }

  getOccasionLabel(): string {
    return this.giftIdea.occasion;
  }

  formatPrice(): string {
    if (this.giftIdea.estimatedPrice === null || this.giftIdea.estimatedPrice === undefined) {
      return 'Price not set';
    }
    return `$${this.giftIdea.estimatedPrice.toFixed(2)}`;
  }
}

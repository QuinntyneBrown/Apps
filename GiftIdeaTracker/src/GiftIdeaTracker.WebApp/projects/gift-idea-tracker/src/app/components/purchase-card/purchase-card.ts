import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Purchase, GiftIdea } from '../../models';

@Component({
  selector: 'app-purchase-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    DatePipe
  ],
  templateUrl: './purchase-card.html',
  styleUrl: './purchase-card.scss'
})
export class PurchaseCard {
  @Input() purchase!: Purchase;
  @Input() giftIdea?: GiftIdea;
  @Output() deleteClick = new EventEmitter<Purchase>();

  onDeleteClick(): void {
    this.deleteClick.emit(this.purchase);
  }

  formatPrice(): string {
    return `$${this.purchase.actualPrice.toFixed(2)}`;
  }
}

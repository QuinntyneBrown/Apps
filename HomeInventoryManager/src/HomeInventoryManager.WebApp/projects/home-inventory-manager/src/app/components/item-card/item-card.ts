import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Item, CategoryLabels, RoomLabels } from '../../models';

@Component({
  selector: 'app-item-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './item-card.html',
  styleUrls: ['./item-card.scss']
})
export class ItemCard {
  @Input() item!: Item;
  @Output() edit = new EventEmitter<Item>();
  @Output() delete = new EventEmitter<Item>();
  @Output() viewDetails = new EventEmitter<Item>();

  readonly categoryLabels = CategoryLabels;
  readonly roomLabels = RoomLabels;

  onEdit(): void {
    this.edit.emit(this.item);
  }

  onDelete(): void {
    this.delete.emit(this.item);
  }

  onViewDetails(): void {
    this.viewDetails.emit(this.item);
  }

  formatCurrency(value: number | null | undefined): string {
    if (value === null || value === undefined) return 'N/A';
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
  }
}

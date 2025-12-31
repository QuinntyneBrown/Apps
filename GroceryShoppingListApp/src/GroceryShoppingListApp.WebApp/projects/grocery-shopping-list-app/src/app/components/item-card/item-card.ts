import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { GroceryItem } from '../../models';

@Component({
  selector: 'app-item-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatCheckboxModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './item-card.html',
  styleUrl: './item-card.scss'
})
export class ItemCard {
  @Input({ required: true }) item!: GroceryItem;
  @Output() toggle = new EventEmitter<GroceryItem>();
  @Output() delete = new EventEmitter<GroceryItem>();

  onToggle(): void {
    this.toggle.emit(this.item);
  }

  onDelete(): void {
    this.delete.emit(this.item);
  }
}

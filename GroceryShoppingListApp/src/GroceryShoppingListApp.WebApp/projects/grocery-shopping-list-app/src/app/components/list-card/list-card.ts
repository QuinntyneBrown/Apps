import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { GroceryList } from '../../models';

@Component({
  selector: 'app-list-card',
  standalone: true,
  imports: [CommonModule, RouterModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './list-card.html',
  styleUrl: './list-card.scss'
})
export class ListCard {
  @Input({ required: true }) list!: GroceryList;
  @Output() delete = new EventEmitter<GroceryList>();

  onDelete(event: Event): void {
    event.stopPropagation();
    this.delete.emit(this.list);
  }
}

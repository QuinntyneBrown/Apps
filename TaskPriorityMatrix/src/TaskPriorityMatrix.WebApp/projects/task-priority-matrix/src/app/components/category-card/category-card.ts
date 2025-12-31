import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Category } from '../../models';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './category-card.html',
  styleUrl: './category-card.scss'
})
export class CategoryCard {
  @Input({ required: true }) category!: Category;
  @Output() delete = new EventEmitter<string>();
  @Output() edit = new EventEmitter<Category>();

  onDelete(): void {
    this.delete.emit(this.category.categoryId);
  }

  onEdit(): void {
    this.edit.emit(this.category);
  }
}

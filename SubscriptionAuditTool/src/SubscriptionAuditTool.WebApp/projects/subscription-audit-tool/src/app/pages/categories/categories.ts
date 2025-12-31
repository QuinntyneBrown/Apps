import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CategoryService } from '../../services';
import { Category } from '../../models';
import { CategoryForm } from '../../components';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatDialogModule
  ],
  templateUrl: './categories.html',
  styleUrl: './categories.scss'
})
export class Categories {
  private _categoryService = inject(CategoryService);
  private _dialog = inject(MatDialog);

  categories$ = this._categoryService.categories$;
  displayedColumns = ['name', 'description', 'colorCode', 'subscriptionCount', 'totalMonthlyCost', 'actions'];

  constructor() {
    this._categoryService.getCategories().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(CategoryForm, {
      width: '500px',
      data: { mode: 'create' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._categoryService.getCategories().subscribe();
      }
    });
  }

  openEditDialog(category: Category): void {
    const dialogRef = this._dialog.open(CategoryForm, {
      width: '500px',
      data: { category, mode: 'edit' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._categoryService.getCategories().subscribe();
      }
    });
  }

  deleteCategory(category: Category): void {
    if (confirm(`Are you sure you want to delete the category "${category.name}"?`)) {
      this._categoryService.deleteCategory(category.categoryId).subscribe({
        next: () => {
          this._categoryService.getCategories().subscribe();
        },
        error: (error) => console.error('Error deleting category:', error)
      });
    }
  }
}

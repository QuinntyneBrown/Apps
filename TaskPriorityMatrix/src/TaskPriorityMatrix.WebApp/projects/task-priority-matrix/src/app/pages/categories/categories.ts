import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Category } from '../../models';
import { CategoryService } from '../../services';
import { CategoryCard } from '../../components/category-card';
import { CreateCategoryDialog } from '../../components/create-category-dialog';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogModule, CategoryCard],
  templateUrl: './categories.html',
  styleUrl: './categories.scss'
})
export class Categories implements OnInit {
  private _categoryService = inject(CategoryService);
  private _dialog = inject(MatDialog);

  categories$ = this._categoryService.categories$;

  ngOnInit(): void {
    this._categoryService.getAll().subscribe();
  }

  onCreateCategory(): void {
    const dialogRef = this._dialog.open(CreateCategoryDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._categoryService.create(result).subscribe();
      }
    });
  }

  onEditCategory(category: Category): void {
    const dialogRef = this._dialog.open(CreateCategoryDialog, {
      width: '500px',
      data: { category }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._categoryService.update({
          categoryId: category.categoryId,
          ...result
        }).subscribe();
      }
    });
  }

  onDeleteCategory(categoryId: string): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this._categoryService.delete(categoryId).subscribe();
    }
  }
}

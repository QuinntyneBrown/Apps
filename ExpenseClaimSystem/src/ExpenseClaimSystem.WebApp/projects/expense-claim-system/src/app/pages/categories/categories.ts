import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { CategoriesService } from '../../services';
import { CategoryDialog } from '../../components';

@Component({
  selector: 'app-categories',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogModule, MatTableModule, MatChipsModule],
  templateUrl: './categories.html',
  styleUrl: './categories.scss',
})
export class Categories implements OnInit {
  private readonly _categoriesService = inject(CategoriesService);
  private readonly _dialog = inject(MatDialog);

  displayedColumns = ['name', 'description', 'maxAmount', 'requiresReceipt', 'isActive', 'actions'];

  categories$ = this._categoriesService.categories$;

  ngOnInit(): void {
    this._categoriesService.getAll().subscribe();
  }

  openDialog(category?: any): void {
    const dialogRef = this._dialog.open(CategoryDialog, {
      width: '600px',
      data: { category },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (category) {
          this._categoriesService.update(category.categoryId, result).subscribe();
        } else {
          this._categoriesService.create(result).subscribe();
        }
      }
    });
  }

  deleteCategory(id: string): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this._categoriesService.delete(id).subscribe();
    }
  }
}

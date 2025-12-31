import { Component, OnInit, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { DocumentCategoryService } from '../services';
import { DocumentCategory } from '../models';

@Component({
  selector: 'app-category-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.category ? 'Edit Category' : 'Create Category' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="category-form">
        <mat-form-field class="category-form__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="category-form__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="onSubmit()" [disabled]="form.invalid">
        {{ data.category ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .category-form {
      display: flex;
      flex-direction: column;
      min-width: 400px;
      padding: 1rem 0;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class CategoryFormDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<CategoryFormDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { category?: DocumentCategory }
  ) {
    this.form = this.fb.group({
      name: [data.category?.name || '', Validators.required],
      description: [data.category?.description || '']
    });
  }

  onSubmit() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule
  ],
  template: `
    <div class="categories">
      <mat-card class="categories__card">
        <mat-card-header class="categories__header">
          <mat-card-title>Document Categories</mat-card-title>
          <button mat-raised-button color="primary" (click)="openCreateDialog()">
            <mat-icon>add</mat-icon>
            Add Category
          </button>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="categories$ | async" class="categories__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let category">{{ category.name }}</td>
            </ng-container>

            <ng-container matColumnDef="description">
              <th mat-header-cell *matHeaderCellDef>Description</th>
              <td mat-cell *matCellDef="let category">{{ category.description || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created At</th>
              <td mat-cell *matCellDef="let category">{{ category.createdAt | date }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let category">
                <button mat-icon-button color="primary" (click)="openEditDialog(category)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteCategory(category.documentCategoryId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .categories {
      padding: 2rem;

      &__card {
        margin-bottom: 2rem;
      }

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 1rem;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class Categories implements OnInit {
  categories$ = this.categoryService.categories$;
  displayedColumns: string[] = ['name', 'description', 'createdAt', 'actions'];

  constructor(
    private categoryService: DocumentCategoryService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.categoryService.getDocumentCategories().subscribe();
  }

  openCreateDialog() {
    const dialogRef = this.dialog.open(CategoryFormDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.categoryService.createDocumentCategory(result).subscribe({
          next: () => {
            this.snackBar.open('Category created successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Error creating category', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditDialog(category: DocumentCategory) {
    const dialogRef = this.dialog.open(CategoryFormDialog, {
      width: '500px',
      data: { category }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const command = {
          ...result,
          documentCategoryId: category.documentCategoryId
        };
        this.categoryService.updateDocumentCategory(category.documentCategoryId, command).subscribe({
          next: () => {
            this.snackBar.open('Category updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Error updating category', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteCategory(id: string) {
    if (confirm('Are you sure you want to delete this category?')) {
      this.categoryService.deleteDocumentCategory(id).subscribe({
        next: () => {
          this.snackBar.open('Category deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting category', 'Close', { duration: 3000 });
        }
      });
    }
  }
}

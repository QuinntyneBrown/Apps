import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Category } from '../../models';
import { CategoryService } from '../../services';

export interface CategoryFormData {
  category?: Category;
  mode: 'create' | 'edit';
}

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './category-form.html',
  styleUrl: './category-form.scss'
})
export class CategoryForm implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<CategoryForm>);
  private _data = inject<CategoryFormData>(MAT_DIALOG_DATA);
  private _categoryService = inject(CategoryService);

  form!: FormGroup;

  get isEditMode(): boolean {
    return this._data.mode === 'edit';
  }

  get title(): string {
    return this.isEditMode ? 'Edit Category' : 'Add Category';
  }

  ngOnInit(): void {
    this.form = this._fb.group({
      name: [this._data.category?.name || '', Validators.required],
      description: [this._data.category?.description || ''],
      colorCode: [this._data.category?.colorCode || '']
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;

    if (this.isEditMode && this._data.category) {
      const updateRequest = {
        categoryId: this._data.category.categoryId,
        ...formValue
      };
      this._categoryService.updateCategory(updateRequest).subscribe({
        next: (result) => this._dialogRef.close(result),
        error: (error) => console.error('Error updating category:', error)
      });
    } else {
      this._categoryService.createCategory(formValue).subscribe({
        next: (result) => this._dialogRef.close(result),
        error: (error) => console.error('Error creating category:', error)
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

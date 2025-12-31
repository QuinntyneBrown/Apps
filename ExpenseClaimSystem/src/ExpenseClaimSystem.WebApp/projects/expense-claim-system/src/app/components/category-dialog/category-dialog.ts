import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Category } from '../../models';

@Component({
  selector: 'app-category-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
  ],
  templateUrl: './category-dialog.html',
  styleUrl: './category-dialog.scss',
})
export class CategoryDialog {
  private readonly _fb = inject(FormBuilder);
  private readonly _dialogRef = inject(MatDialogRef<CategoryDialog>);

  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { category?: Category }) {
    this.form = this._fb.group({
      name: [data.category?.name || '', Validators.required],
      description: [data.category?.description || ''],
      maxAmount: [data.category?.maxAmount || null, Validators.min(0)],
      requiresReceipt: [data.category?.requiresReceipt ?? true],
      isActive: [data.category?.isActive ?? true],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this._dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

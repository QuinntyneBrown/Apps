import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { Category } from '../../models';

export interface CreateItemDialogData {
  groceryListId: string;
}

@Component({
  selector: 'app-create-item-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './create-item-dialog.html',
  styleUrl: './create-item-dialog.scss'
})
export class CreateItemDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<CreateItemDialog>);
  private _data = inject<CreateItemDialogData>(MAT_DIALOG_DATA);

  categories = Object.values(Category);

  form = this._fb.group({
    name: ['', [Validators.required, Validators.maxLength(255)]],
    category: [Category.Other, Validators.required],
    quantity: [1, [Validators.required, Validators.min(1)]]
  });

  onSubmit(): void {
    if (this.form.valid) {
      this._dialogRef.close({
        ...this.form.value,
        groceryListId: this._data.groceryListId
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

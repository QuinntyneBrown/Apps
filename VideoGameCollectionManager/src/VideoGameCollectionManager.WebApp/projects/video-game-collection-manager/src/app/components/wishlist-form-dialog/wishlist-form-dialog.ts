import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Wishlist, Platform, Genre } from '../../models';

@Component({
  selector: 'app-wishlist-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  templateUrl: './wishlist-form-dialog.html',
  styleUrl: './wishlist-form-dialog.scss'
})
export class WishlistFormDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<WishlistFormDialog>);

  platforms = Object.values(Platform);
  genres = Object.values(Genre);

  wishlistForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { wishlist?: Wishlist }) {
    this.wishlistForm = this._fb.group({
      title: [data.wishlist?.title || '', Validators.required],
      platform: [data.wishlist?.platform || null],
      genre: [data.wishlist?.genre || null],
      priority: [data.wishlist?.priority || 1, [Validators.required, Validators.min(1), Validators.max(10)]],
      notes: [data.wishlist?.notes || ''],
      isAcquired: [data.wishlist?.isAcquired || false]
    });
  }

  onSubmit(): void {
    if (this.wishlistForm.valid) {
      this._dialogRef.close(this.wishlistForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

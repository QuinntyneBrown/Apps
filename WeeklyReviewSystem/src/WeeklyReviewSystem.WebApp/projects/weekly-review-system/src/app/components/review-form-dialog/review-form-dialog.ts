import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSliderModule } from '@angular/material/slider';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { WeeklyReview } from '../../models';

@Component({
  selector: 'app-review-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSliderModule,
    MatCheckboxModule
  ],
  templateUrl: './review-form-dialog.html',
  styleUrl: './review-form-dialog.scss'
})
export class ReviewFormDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<ReviewFormDialog>);

  reviewForm: FormGroup;
  isEditMode: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { review?: WeeklyReview }) {
    this.isEditMode = !!data?.review;

    this.reviewForm = this._fb.group({
      weekStartDate: [data?.review?.weekStartDate || '', Validators.required],
      weekEndDate: [data?.review?.weekEndDate || '', Validators.required],
      overallRating: [data?.review?.overallRating || 5],
      reflections: [data?.review?.reflections || ''],
      lessonsLearned: [data?.review?.lessonsLearned || ''],
      gratitude: [data?.review?.gratitude || ''],
      improvementAreas: [data?.review?.improvementAreas || ''],
      isCompleted: [data?.review?.isCompleted || false]
    });
  }

  onSubmit(): void {
    if (this.reviewForm.valid) {
      this._dialogRef.close(this.reviewForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

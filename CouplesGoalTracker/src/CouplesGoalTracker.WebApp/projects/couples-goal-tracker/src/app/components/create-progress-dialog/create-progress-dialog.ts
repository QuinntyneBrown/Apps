import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { CreateProgress } from '../../models';

@Component({
  selector: 'app-create-progress-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSlideToggleModule,
    MatSliderModule
  ],
  templateUrl: './create-progress-dialog.html',
  styleUrl: './create-progress-dialog.scss'
})
export class CreateProgressDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CreateProgressDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { goalId: string; userId: string }
  ) {
    this.form = this.fb.group({
      goalId: [data.goalId, Validators.required],
      userId: [data.userId, Validators.required],
      progressDate: [new Date(), Validators.required],
      notes: ['', Validators.required],
      completionPercentage: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      effortHours: [null, Validators.min(0)],
      isSignificant: [false]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const progress: CreateProgress = {
        ...formValue,
        progressDate: formValue.progressDate.toISOString()
      };
      this.dialogRef.close(progress);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

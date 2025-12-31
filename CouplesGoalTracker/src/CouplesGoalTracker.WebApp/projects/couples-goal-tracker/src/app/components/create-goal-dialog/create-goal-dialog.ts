import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { GoalCategory, GoalCategoryLabels, CreateGoal } from '../../models';

@Component({
  selector: 'app-create-goal-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSlideToggleModule,
    MatSliderModule
  ],
  templateUrl: './create-goal-dialog.html',
  styleUrl: './create-goal-dialog.scss'
})
export class CreateGoalDialog {
  form: FormGroup;
  categories = Object.keys(GoalCategory).filter(k => !isNaN(Number(k))).map(k => ({
    value: Number(k),
    label: GoalCategoryLabels[Number(k) as GoalCategory]
  }));

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CreateGoalDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { userId: string }
  ) {
    this.form = this.fb.group({
      userId: [data.userId, Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      category: [GoalCategory.Other, Validators.required],
      targetDate: [null],
      priority: [3, [Validators.required, Validators.min(1), Validators.max(5)]],
      isShared: [true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const goal: CreateGoal = {
        ...formValue,
        targetDate: formValue.targetDate ? formValue.targetDate.toISOString() : null
      };
      this.dialogRef.close(goal);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

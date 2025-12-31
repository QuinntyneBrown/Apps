import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Habit, HabitFrequency, CreateHabitRequest, UpdateHabitRequest } from '../../models';

export interface HabitFormDialogData {
  habit?: Habit;
  userId: string;
}

@Component({
  selector: 'app-habit-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './habit-form-dialog.html',
  styleUrl: './habit-form-dialog.scss'
})
export class HabitFormDialog {
  form: FormGroup;
  isEditMode: boolean;
  frequencies = [
    { value: HabitFrequency.Daily, label: 'Daily' },
    { value: HabitFrequency.Weekly, label: 'Weekly' },
    { value: HabitFrequency.Custom, label: 'Custom' }
  ];

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<HabitFormDialog>,
    @Inject(MAT_DIALOG_DATA) public data: HabitFormDialogData
  ) {
    this.isEditMode = !!data.habit;
    this.form = this.fb.group({
      name: [data.habit?.name || '', [Validators.required, Validators.maxLength(100)]],
      description: [data.habit?.description || '', Validators.maxLength(500)],
      frequency: [data.habit?.frequency || HabitFrequency.Daily, Validators.required],
      targetDaysPerWeek: [data.habit?.targetDaysPerWeek || 7, [Validators.required, Validators.min(1), Validators.max(7)]],
      startDate: [data.habit ? new Date(data.habit.startDate) : new Date(), Validators.required],
      notes: [data.habit?.notes || '', Validators.maxLength(500)]
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.data.habit) {
        const request: UpdateHabitRequest = {
          habitId: this.data.habit.habitId,
          name: formValue.name,
          description: formValue.description,
          frequency: formValue.frequency,
          targetDaysPerWeek: formValue.targetDaysPerWeek,
          notes: formValue.notes
        };
        this.dialogRef.close(request);
      } else {
        const request: CreateHabitRequest = {
          userId: this.data.userId,
          name: formValue.name,
          description: formValue.description,
          frequency: formValue.frequency,
          targetDaysPerWeek: formValue.targetDaysPerWeek,
          startDate: formValue.startDate.toISOString(),
          notes: formValue.notes
        };
        this.dialogRef.close(request);
      }
    }
  }
}

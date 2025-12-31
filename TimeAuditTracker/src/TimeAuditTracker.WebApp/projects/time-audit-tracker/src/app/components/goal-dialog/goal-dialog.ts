import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Goal, ActivityCategory } from '../../models';

@Component({
  selector: 'app-goal-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './goal-dialog.html',
  styleUrl: './goal-dialog.scss'
})
export class GoalDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<GoalDialog>);

  form: FormGroup;
  categories = Object.values(ActivityCategory);
  isEditMode: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { goal?: Goal; userId: string }) {
    this.isEditMode = !!data.goal;

    this.form = this._fb.group({
      category: [data.goal?.category || ActivityCategory.Work, Validators.required],
      description: [data.goal?.description || '', Validators.required],
      targetHoursPerWeek: [data.goal?.targetHoursPerWeek || 0, [Validators.required, Validators.min(0)]],
      minimumHoursPerWeek: [data.goal?.minimumHoursPerWeek || null, Validators.min(0)],
      startDate: [data.goal?.startDate ? new Date(data.goal.startDate) : new Date(), Validators.required],
      endDate: [data.goal?.endDate ? new Date(data.goal.endDate) : null],
      isActive: [data.goal?.isActive ?? true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        userId: this.data.userId
      };
      this._dialogRef.close(result);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

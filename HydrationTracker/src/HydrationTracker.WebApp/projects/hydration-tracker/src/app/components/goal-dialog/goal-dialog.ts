import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Goal, CreateGoalCommand, UpdateGoalCommand } from '../../models';

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
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule
  ],
  templateUrl: './goal-dialog.html',
  styleUrls: ['./goal-dialog.scss']
})
export class GoalDialog implements OnInit {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<GoalDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { goal?: Goal }
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      userId: ['00000000-0000-0000-0000-000000000000', Validators.required],
      dailyGoalMl: [this.data.goal?.dailyGoalMl ?? 2000, [Validators.required, Validators.min(1)]],
      startDate: [this.data.goal?.startDate ?? new Date(), Validators.required],
      isActive: [this.data.goal?.isActive ?? true],
      notes: [this.data.goal?.notes ?? '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const command: CreateGoalCommand | UpdateGoalCommand = this.data.goal
        ? { ...formValue, goalId: this.data.goal.goalId }
        : formValue;
      this.dialogRef.close(command);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

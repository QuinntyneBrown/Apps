import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CreateMilestone } from '../../models';

@Component({
  selector: 'app-create-milestone-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './create-milestone-dialog.html',
  styleUrl: './create-milestone-dialog.scss'
})
export class CreateMilestoneDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CreateMilestoneDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { goalId: string; userId: string; sortOrder: number }
  ) {
    this.form = this.fb.group({
      goalId: [data.goalId, Validators.required],
      userId: [data.userId, Validators.required],
      title: ['', Validators.required],
      description: [''],
      targetDate: [null],
      sortOrder: [data.sortOrder, Validators.required]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const milestone: CreateMilestone = {
        ...formValue,
        targetDate: formValue.targetDate ? formValue.targetDate.toISOString() : null
      };
      this.dialogRef.close(milestone);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

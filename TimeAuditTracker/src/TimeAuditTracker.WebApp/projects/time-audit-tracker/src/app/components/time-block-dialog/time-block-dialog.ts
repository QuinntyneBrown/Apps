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
import { TimeBlock, ActivityCategory } from '../../models';

@Component({
  selector: 'app-time-block-dialog',
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
  templateUrl: './time-block-dialog.html',
  styleUrl: './time-block-dialog.scss'
})
export class TimeBlockDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<TimeBlockDialog>);

  form: FormGroup;
  categories = Object.values(ActivityCategory);
  isEditMode: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { timeBlock?: TimeBlock; userId: string }) {
    this.isEditMode = !!data.timeBlock;

    this.form = this._fb.group({
      category: [data.timeBlock?.category || ActivityCategory.Work, Validators.required],
      description: [data.timeBlock?.description || '', Validators.required],
      startTime: [data.timeBlock?.startTime ? new Date(data.timeBlock.startTime) : new Date(), Validators.required],
      endTime: [data.timeBlock?.endTime ? new Date(data.timeBlock.endTime) : null],
      notes: [data.timeBlock?.notes || ''],
      tags: [data.timeBlock?.tags || ''],
      isProductive: [data.timeBlock?.isProductive ?? true]
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

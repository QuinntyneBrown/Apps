import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Reading } from '../../models';

export interface ReadingDialogData {
  reading?: Reading;
  userId: string;
}

@Component({
  selector: 'app-reading-dialog',
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
  templateUrl: './reading-dialog.html',
  styleUrl: './reading-dialog.scss'
})
export class ReadingDialog implements OnInit {
  readingForm!: FormGroup;
  isEditMode = false;

  positions = ['Sitting', 'Standing', 'Lying Down'];
  arms = ['Left', 'Right'];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ReadingDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ReadingDialogData
  ) {
    this.isEditMode = !!data.reading;
  }

  ngOnInit(): void {
    this.readingForm = this.fb.group({
      systolic: [
        this.data.reading?.systolic || '',
        [Validators.required, Validators.min(70), Validators.max(250)]
      ],
      diastolic: [
        this.data.reading?.diastolic || '',
        [Validators.required, Validators.min(40), Validators.max(150)]
      ],
      pulse: [
        this.data.reading?.pulse || '',
        [Validators.min(40), Validators.max(200)]
      ],
      measuredAt: [
        this.data.reading?.measuredAt ? new Date(this.data.reading.measuredAt) : new Date(),
        [Validators.required]
      ],
      position: [this.data.reading?.position || ''],
      arm: [this.data.reading?.arm || ''],
      notes: [this.data.reading?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.readingForm.valid) {
      const formValue = this.readingForm.value;
      const result = {
        ...formValue,
        measuredAt: formValue.measuredAt.toISOString(),
        userId: this.data.userId,
        ...(this.isEditMode && { readingId: this.data.reading!.readingId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ValueEstimate, CreateValueEstimateCommand, UpdateValueEstimateCommand } from '../../models';

export interface ValueEstimateDialogData {
  estimate?: ValueEstimate;
  itemId: string;
}

@Component({
  selector: 'app-value-estimate-dialog',
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
  templateUrl: './value-estimate-dialog.html',
  styleUrls: ['./value-estimate-dialog.scss']
})
export class ValueEstimateDialog implements OnInit {
  form!: FormGroup;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ValueEstimateDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ValueEstimateDialogData
  ) {
    this.isEditMode = !!data.estimate;
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      estimatedValue: [
        this.data.estimate?.estimatedValue ?? null,
        [Validators.required, Validators.min(0)]
      ],
      estimationDate: [
        this.data.estimate?.estimationDate ? new Date(this.data.estimate.estimationDate) : new Date(),
        Validators.required
      ],
      source: [this.data.estimate?.source || ''],
      notes: [this.data.estimate?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.data.estimate) {
        const command: UpdateValueEstimateCommand = {
          valueEstimateId: this.data.estimate.valueEstimateId,
          estimatedValue: formValue.estimatedValue,
          estimationDate: formValue.estimationDate.toISOString(),
          source: formValue.source || null,
          notes: formValue.notes || null
        };
        this.dialogRef.close(command);
      } else {
        const command: CreateValueEstimateCommand = {
          itemId: this.data.itemId,
          estimatedValue: formValue.estimatedValue,
          estimationDate: formValue.estimationDate.toISOString(),
          source: formValue.source || null,
          notes: formValue.notes || null
        };
        this.dialogRef.close(command);
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

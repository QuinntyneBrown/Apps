import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { Receipt, Deduction } from '../../models';

export interface ReceiptFormDialogData {
  receipt?: Receipt;
  deductions: Deduction[];
}

@Component({
  selector: 'app-receipt-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './receipt-form-dialog.html',
  styleUrl: './receipt-form-dialog.scss'
})
export class ReceiptFormDialog {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<ReceiptFormDialog>);

  receiptForm: FormGroup;
  deductions: Deduction[];
  isEdit: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: ReceiptFormDialogData) {
    this.deductions = data.deductions;
    this.isEdit = !!data.receipt;

    this.receiptForm = this._fb.group({
      receiptId: [data.receipt?.receiptId || ''],
      deductionId: [data.receipt?.deductionId || '', Validators.required],
      fileName: [data.receipt?.fileName || '', Validators.required],
      fileUrl: [data.receipt?.fileUrl || '', Validators.required],
      notes: [data.receipt?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.receiptForm.valid) {
      this._dialogRef.close(this.receiptForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

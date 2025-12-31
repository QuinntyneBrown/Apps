import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Bill, BillingFrequency, BillStatus, Payee } from '../../models';

@Component({
  selector: 'app-bill-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
  ],
  templateUrl: './bill-dialog.html',
  styleUrl: './bill-dialog.scss',
})
export class BillDialog {
  private readonly _fb = inject(FormBuilder);
  private readonly _dialogRef = inject(MatDialogRef<BillDialog>);

  form: FormGroup;
  billingFrequencies = Object.values(BillingFrequency);
  billStatuses = Object.values(BillStatus);

  constructor(@Inject(MAT_DIALOG_DATA) public data: { bill?: Bill; payees: Payee[] }) {
    this.form = this._fb.group({
      payeeId: [data.bill?.payeeId || '', Validators.required],
      name: [data.bill?.name || '', Validators.required],
      amount: [data.bill?.amount || 0, [Validators.required, Validators.min(0)]],
      dueDate: [data.bill?.dueDate ? new Date(data.bill.dueDate) : new Date(), Validators.required],
      billingFrequency: [data.bill?.billingFrequency || BillingFrequency.Monthly, Validators.required],
      status: [data.bill?.status || BillStatus.Pending, Validators.required],
      isAutoPay: [data.bill?.isAutoPay || false],
      notes: [data.bill?.notes || ''],
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this._dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}

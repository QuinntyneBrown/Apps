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
import { Payment, Bill } from '../../models';

@Component({
  selector: 'app-payment-dialog',
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
  ],
  templateUrl: './payment-dialog.html',
  styleUrl: './payment-dialog.scss',
})
export class PaymentDialog {
  private readonly _fb = inject(FormBuilder);
  private readonly _dialogRef = inject(MatDialogRef<PaymentDialog>);

  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { payment?: Payment; bills: Bill[] }) {
    this.form = this._fb.group({
      billId: [data.payment?.billId || '', Validators.required],
      amount: [data.payment?.amount || 0, [Validators.required, Validators.min(0)]],
      paymentDate: [data.payment?.paymentDate ? new Date(data.payment.paymentDate) : new Date(), Validators.required],
      confirmationNumber: [data.payment?.confirmationNumber || ''],
      paymentMethod: [data.payment?.paymentMethod || ''],
      notes: [data.payment?.notes || ''],
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

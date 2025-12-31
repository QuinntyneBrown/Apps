import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Payee } from '../../models';

@Component({
  selector: 'app-payee-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './payee-dialog.html',
  styleUrl: './payee-dialog.scss',
})
export class PayeeDialog {
  private readonly _fb = inject(FormBuilder);
  private readonly _dialogRef = inject(MatDialogRef<PayeeDialog>);

  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { payee?: Payee }) {
    this.form = this._fb.group({
      name: [data.payee?.name || '', Validators.required],
      accountNumber: [data.payee?.accountNumber || ''],
      website: [data.payee?.website || ''],
      phoneNumber: [data.payee?.phoneNumber || ''],
      notes: [data.payee?.notes || ''],
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

import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { DigitalAccount, AccountType } from '../../models';

@Component({
  selector: 'app-digital-account-dialog',
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
  templateUrl: './digital-account-dialog.html',
  styleUrl: './digital-account-dialog.scss'
})
export class DigitalAccountDialog {
  form: FormGroup;
  accountTypes = Object.keys(AccountType).filter(k => !isNaN(Number(k))).map(k => ({
    value: Number(k),
    label: AccountType[Number(k)]
  }));

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<DigitalAccountDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { account?: DigitalAccount; userId: string }
  ) {
    this.form = this.fb.group({
      accountType: [data.account?.accountType ?? 0, Validators.required],
      accountName: [data.account?.accountName ?? '', Validators.required],
      username: [data.account?.username ?? '', Validators.required],
      passwordHint: [data.account?.passwordHint ?? ''],
      url: [data.account?.url ?? ''],
      desiredAction: [data.account?.desiredAction ?? ''],
      notes: [data.account?.notes ?? '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

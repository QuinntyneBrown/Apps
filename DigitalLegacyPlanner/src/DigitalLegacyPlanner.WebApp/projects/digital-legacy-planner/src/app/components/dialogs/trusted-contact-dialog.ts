import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { TrustedContact } from '../../models';

@Component({
  selector: 'app-trusted-contact-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  templateUrl: './trusted-contact-dialog.html',
  styleUrl: './trusted-contact-dialog.scss'
})
export class TrustedContactDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<TrustedContactDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { contact?: TrustedContact; userId: string }
  ) {
    this.form = this.fb.group({
      fullName: [data.contact?.fullName ?? '', Validators.required],
      relationship: [data.contact?.relationship ?? '', Validators.required],
      email: [data.contact?.email ?? '', [Validators.required, Validators.email]],
      phoneNumber: [data.contact?.phoneNumber ?? ''],
      role: [data.contact?.role ?? ''],
      isPrimaryContact: [data.contact?.isPrimaryContact ?? false],
      notes: [data.contact?.notes ?? '']
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

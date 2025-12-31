import { Component, Inject, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Employee } from '../../models';

@Component({
  selector: 'app-employee-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
  ],
  templateUrl: './employee-dialog.html',
  styleUrl: './employee-dialog.scss',
})
export class EmployeeDialog {
  private readonly _fb = inject(FormBuilder);
  private readonly _dialogRef = inject(MatDialogRef<EmployeeDialog>);

  form: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { employee?: Employee }) {
    this.form = this._fb.group({
      firstName: [data.employee?.firstName || '', Validators.required],
      lastName: [data.employee?.lastName || '', Validators.required],
      email: [data.employee?.email || '', [Validators.required, Validators.email]],
      department: [data.employee?.department || ''],
      position: [data.employee?.position || ''],
      isActive: [data.employee?.isActive ?? true],
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

import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Business } from '../../models';
import { BusinessService } from '../../services';

@Component({
  selector: 'app-business-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './business-form-dialog.html',
  styleUrl: './business-form-dialog.scss'
})
export class BusinessFormDialog {
  private _fb = inject(FormBuilder);
  private _businessService = inject(BusinessService);
  private _dialogRef = inject(MatDialogRef<BusinessFormDialog>);

  form: FormGroup;
  isEditMode: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { business?: Business }) {
    this.isEditMode = !!data?.business;

    this.form = this._fb.group({
      name: [data?.business?.name || '', Validators.required],
      description: [data?.business?.description || ''],
      startDate: [data?.business?.startDate ? new Date(data.business.startDate) : new Date(), Validators.required],
      isActive: [data?.business?.isActive ?? true],
      taxId: [data?.business?.taxId || ''],
      notes: [data?.business?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const businessData = {
      ...formValue,
      startDate: formValue.startDate.toISOString()
    };

    if (this.isEditMode && this.data.business) {
      this._businessService.update(this.data.business.businessId, businessData).subscribe({
        next: () => this._dialogRef.close(true),
        error: (error) => console.error('Error updating business:', error)
      });
    } else {
      this._businessService.create(businessData).subscribe({
        next: () => this._dialogRef.close(true),
        error: (error) => console.error('Error creating business:', error)
      });
    }
  }

  onCancel(): void {
    this._dialogRef.close(false);
  }
}

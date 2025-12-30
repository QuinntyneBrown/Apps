import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { HouseholdDto, CanadianProvince, CANADIAN_PROVINCES } from '../../models/household-dto';
import { CreateHouseholdCommand } from '../../models/create-household-command';

export interface CreateOrEditHouseholdDialogData {
  household?: HouseholdDto;
}

export interface CreateOrEditHouseholdDialogResult {
  action: 'create' | 'update' | 'cancel';
  data?: CreateHouseholdCommand & { householdId?: string };
}

@Component({
  selector: 'app-create-or-edit-household-dialog',
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
  templateUrl: './create-or-edit-household-dialog.html',
  styleUrls: ['./create-or-edit-household-dialog.scss']
})
export class CreateOrEditHouseholdDialog {
  private fb = inject(FormBuilder);

  form: FormGroup;

  availableProvinces = CANADIAN_PROVINCES;

  constructor(
    public dialogRef: MatDialogRef<CreateOrEditHouseholdDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CreateOrEditHouseholdDialogData
  ) {
    this.form = this.fb.group({
      name: [data.household?.name || '', Validators.required],
      street: [data.household?.street || '', Validators.required],
      city: [data.household?.city || '', Validators.required],
      province: [data.household?.province || 'Ontario', Validators.required],
      postalCode: [data.household?.postalCode || '', [Validators.required, Validators.pattern(/^[A-Za-z]\d[A-Za-z][ ]?\d[A-Za-z]\d$/)]]
    });
  }

  get isEditMode(): boolean {
    return !!this.data.household;
  }

  get dialogTitle(): string {
    return this.isEditMode ? 'Edit Household' : 'Create Household';
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      const result: CreateOrEditHouseholdDialogResult = {
        action: this.isEditMode ? 'update' : 'create',
        data: {
          name: formValue.name,
          street: formValue.street,
          city: formValue.city,
          province: formValue.province,
          postalCode: formValue.postalCode,
          ...(this.isEditMode && { householdId: this.data.household!.householdId })
        }
      };

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close({ action: 'cancel' } as CreateOrEditHouseholdDialogResult);
  }
}

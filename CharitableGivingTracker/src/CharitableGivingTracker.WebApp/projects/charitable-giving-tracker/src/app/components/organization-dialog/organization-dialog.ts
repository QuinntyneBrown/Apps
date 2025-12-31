import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { Organization } from '../../models';

export interface OrganizationDialogData {
  organization?: Organization;
}

@Component({
  selector: 'app-organization-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  templateUrl: './organization-dialog.html',
  styleUrl: './organization-dialog.scss'
})
export class OrganizationDialog implements OnInit {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<OrganizationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: OrganizationDialogData
  ) {
    this.form = this.fb.group({
      name: [data.organization?.name || '', Validators.required],
      ein: [data.organization?.ein || ''],
      address: [data.organization?.address || ''],
      website: [data.organization?.website || ''],
      is501c3: [data.organization?.is501c3 ?? true],
      notes: [data.organization?.notes || '']
    });
  }

  ngOnInit(): void {}

  get isEditMode(): boolean {
    return !!this.data.organization;
  }

  onSubmit(): void {
    if (this.form.valid) {
      const result = this.form.value;

      if (this.isEditMode) {
        result.organizationId = this.data.organization!.organizationId;
      }

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

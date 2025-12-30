import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { FamilyMemberDto } from '../../models/family-member-dto';
import { CreateFamilyMemberCommand } from '../../models/create-family-member-command';

export interface CreateOrEditFamilyMemberDialogData {
  member?: FamilyMemberDto;
  familyId: string;
}

export interface CreateOrEditFamilyMemberDialogResult {
  action: 'create' | 'update' | 'cancel';
  data?: CreateFamilyMemberCommand & { memberId?: string };
}

@Component({
  selector: 'app-create-or-edit-family-member-dialog',
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
  templateUrl: './create-or-edit-family-member-dialog.html',
  styleUrls: ['./create-or-edit-family-member-dialog.scss']
})
export class CreateOrEditFamilyMemberDialog {
  private fb = inject(FormBuilder);

  form: FormGroup;

  availableColors = [
    { value: '#ef4444', label: 'Red' },
    { value: '#f59e0b', label: 'Orange' },
    { value: '#10b981', label: 'Green' },
    { value: '#3b82f6', label: 'Blue' },
    { value: '#8b5cf6', label: 'Purple' },
    { value: '#ec4899', label: 'Pink' },
    { value: '#06b6d4', label: 'Cyan' },
    { value: '#f97316', label: 'Amber' }
  ];

  availableRoles = [
    { value: 'Admin', label: 'Admin' },
    { value: 'Member', label: 'Member' },
    { value: 'ViewOnly', label: 'View Only' }
  ];

  constructor(
    public dialogRef: MatDialogRef<CreateOrEditFamilyMemberDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CreateOrEditFamilyMemberDialogData
  ) {
    this.form = this.fb.group({
      name: [data.member?.name || '', Validators.required],
      email: [data.member?.email || '', [Validators.required, Validators.email]],
      color: [data.member?.color || '#3b82f6', Validators.required],
      role: [data.member?.role || 'Member', Validators.required]
    });
  }

  get isEditMode(): boolean {
    return !!this.data.member;
  }

  get dialogTitle(): string {
    return this.isEditMode ? 'Edit Family Member' : 'Create Family Member';
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      const result: CreateOrEditFamilyMemberDialogResult = {
        action: this.isEditMode ? 'update' : 'create',
        data: {
          familyId: this.data.familyId,
          name: formValue.name,
          email: formValue.email,
          color: formValue.color,
          role: formValue.role,
          ...(this.isEditMode && { memberId: this.data.member!.memberId })
        }
      };

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close({ action: 'cancel' } as CreateOrEditFamilyMemberDialogResult);
  }
}

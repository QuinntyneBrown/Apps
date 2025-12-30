import { Component, inject, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { RoleDto, CreateRoleCommand } from '../../models/role-dto';

export interface CreateOrEditRoleDialogData {
  role?: RoleDto;
}

export interface CreateOrEditRoleDialogResult {
  action: 'create' | 'update' | 'cancel';
  data?: CreateRoleCommand & { roleId?: string };
}

@Component({
  selector: 'app-create-or-edit-role-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './create-or-edit-role-dialog.html',
  styleUrls: ['./create-or-edit-role-dialog.scss']
})
export class CreateOrEditRoleDialog {
  private fb = inject(FormBuilder);

  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CreateOrEditRoleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CreateOrEditRoleDialogData
  ) {
    this.form = this.fb.group({
      name: [data.role?.name || '', Validators.required]
    });
  }

  get isEditMode(): boolean {
    return !!this.data.role;
  }

  get dialogTitle(): string {
    return this.isEditMode ? 'Edit Role' : 'Create Role';
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      const result: CreateOrEditRoleDialogResult = {
        action: this.isEditMode ? 'update' : 'create',
        data: {
          name: formValue.name,
          ...(this.isEditMode && { roleId: this.data.role!.roleId })
        }
      };

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close({ action: 'cancel' } as CreateOrEditRoleDialogResult);
  }
}

import { Component, inject, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Observable } from 'rxjs';
import { UserDto, CreateUserCommand } from '../../models/user-dto';
import { RoleDto } from '../../models/role-dto';
import { RolesService } from '../../services/roles.service';

export interface CreateOrEditUserDialogData {
  user?: UserDto;
}

export interface CreateOrEditUserDialogResult {
  action: 'create' | 'update' | 'cancel';
  data?: CreateUserCommand & { userId?: string };
}

@Component({
  selector: 'app-create-or-edit-user-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  templateUrl: './create-or-edit-user-dialog.html',
  styleUrls: ['./create-or-edit-user-dialog.scss']
})
export class CreateOrEditUserDialog implements OnInit {
  private fb = inject(FormBuilder);
  private rolesService = inject(RolesService);

  form: FormGroup;
  roles$!: Observable<RoleDto[]>;

  constructor(
    public dialogRef: MatDialogRef<CreateOrEditUserDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CreateOrEditUserDialogData
  ) {
    const isEditMode = !!data.user;
    this.form = this.fb.group({
      userName: [data.user?.userName || '', Validators.required],
      email: [data.user?.email || '', [Validators.required, Validators.email]],
      password: ['', isEditMode ? [] : [Validators.required, Validators.minLength(8)]],
      roleIds: [data.user?.roles?.map(r => r.roleId) || []]
    });
  }

  ngOnInit(): void {
    this.roles$ = this.rolesService.getRoles();
  }

  get isEditMode(): boolean {
    return !!this.data.user;
  }

  get dialogTitle(): string {
    return this.isEditMode ? 'Edit User' : 'Create User';
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      const result: CreateOrEditUserDialogResult = {
        action: this.isEditMode ? 'update' : 'create',
        data: {
          userName: formValue.userName,
          email: formValue.email,
          password: formValue.password || undefined,
          roleIds: formValue.roleIds,
          ...(this.isEditMode && { userId: this.data.user!.userId })
        }
      };

      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close({ action: 'cancel' } as CreateOrEditUserDialogResult);
  }
}

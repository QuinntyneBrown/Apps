import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { BehaviorSubject, switchMap } from 'rxjs';
import { RolesService } from '../../services/roles.service';
import { RoleDto } from '../../models/role-dto';
import { CreateOrEditRoleDialog, CreateOrEditRoleDialogResult } from '../../components/create-or-edit-role-dialog';
import { ConfirmDialog, ConfirmDialogResult } from '../../components/confirm-dialog';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule,
    MatSnackBarModule
  ],
  templateUrl: './roles.html',
  styleUrls: ['./roles.scss']
})
export class Roles {
  private rolesService = inject(RolesService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  private refresh$ = new BehaviorSubject<void>(undefined);

  displayedColumns: string[] = ['name', 'actions'];

  roles$ = this.refresh$.pipe(
    switchMap(() => this.rolesService.getRoles())
  );

  onCreateRole(): void {
    const dialogRef = this.dialog.open(CreateOrEditRoleDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditRoleDialogResult) => {
      if (result?.action === 'create' && result.data) {
        this.rolesService.createRole(result.data).subscribe({
          next: () => {
            this.refresh$.next();
            this.snackBar.open('Role created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error creating role:', error);
            this.snackBar.open('Error creating role', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEditRole(role: RoleDto): void {
    const dialogRef = this.dialog.open(CreateOrEditRoleDialog, {
      width: '500px',
      data: { role }
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditRoleDialogResult) => {
      if (result?.action === 'update' && result.data) {
        this.rolesService.updateRole(role.roleId, {
          roleId: role.roleId,
          name: result.data.name
        }).subscribe({
          next: () => {
            this.refresh$.next();
            this.snackBar.open('Role updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error updating role:', error);
            this.snackBar.open('Error updating role', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteRole(role: RoleDto): void {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      width: '400px',
      data: {
        title: 'Delete Role',
        message: `Are you sure you want to delete ${role.name}? This will remove this role from all users.`
      }
    });

    dialogRef.afterClosed().subscribe((result: ConfirmDialogResult) => {
      if (result?.confirmed) {
        this.rolesService.deleteRole(role.roleId).subscribe({
          next: () => {
            this.refresh$.next();
            this.snackBar.open('Role deleted successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error deleting role:', error);
            this.snackBar.open('Error deleting role', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }
}

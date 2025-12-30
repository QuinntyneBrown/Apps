import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { BehaviorSubject, switchMap } from 'rxjs';
import { RolesService } from '../../services/roles.service';
import { RoleDto } from '../../models/role-dto';
import { CreateOrEditRoleDialog, CreateOrEditRoleDialogResult } from '../../components/create-or-edit-role-dialog';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule
  ],
  templateUrl: './roles.html',
  styleUrls: ['./roles.scss']
})
export class Roles {
  private rolesService = inject(RolesService);
  private dialog = inject(MatDialog);

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
          },
          error: (error) => {
            console.error('Error creating role:', error);
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
          },
          error: (error) => {
            console.error('Error updating role:', error);
          }
        });
      }
    });
  }

  onDeleteRole(role: RoleDto): void {
    if (confirm(`Are you sure you want to delete ${role.name}? This will remove this role from all users.`)) {
      this.rolesService.deleteRole(role.roleId).subscribe({
        next: () => {
          this.refresh$.next();
        },
        error: (error) => {
          console.error('Error deleting role:', error);
        }
      });
    }
  }
}

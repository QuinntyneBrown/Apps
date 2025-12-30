import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { BehaviorSubject, switchMap, forkJoin, of } from 'rxjs';
import { UsersService } from '../../services/users.service';
import { RolesService } from '../../services/roles.service';
import { UserDto, RoleDto } from '../../models/user-dto';
import { CreateOrEditUserDialog, CreateOrEditUserDialogResult } from '../../components/create-or-edit-user-dialog';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule,
    MatMenuModule
  ],
  templateUrl: './users.html',
  styleUrls: ['./users.scss']
})
export class Users {
  private usersService = inject(UsersService);
  private rolesService = inject(RolesService);
  private dialog = inject(MatDialog);

  private refresh$ = new BehaviorSubject<void>(undefined);

  displayedColumns: string[] = ['userName', 'email', 'roles', 'actions'];

  users$ = this.refresh$.pipe(
    switchMap(() => this.usersService.getUsers())
  );

  allRoles$ = this.rolesService.getRoles();

  onCreateUser(): void {
    const dialogRef = this.dialog.open(CreateOrEditUserDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditUserDialogResult) => {
      if (result?.action === 'create' && result.data) {
        this.usersService.createUser(result.data).subscribe({
          next: () => {
            this.refresh$.next();
          },
          error: (error) => {
            console.error('Error creating user:', error);
          }
        });
      }
    });
  }

  onEditUser(user: UserDto): void {
    const dialogRef = this.dialog.open(CreateOrEditUserDialog, {
      width: '500px',
      data: { user }
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditUserDialogResult) => {
      if (result?.action === 'update' && result.data) {
        this.usersService.updateUser(user.userId, {
          userId: user.userId,
          userName: result.data.userName,
          email: result.data.email,
          password: result.data.password
        }).subscribe({
          next: () => {
            this.refresh$.next();
          },
          error: (error) => {
            console.error('Error updating user:', error);
          }
        });
      }
    });
  }

  onDeleteUser(user: UserDto): void {
    if (confirm(`Are you sure you want to delete ${user.userName}?`)) {
      this.usersService.deleteUser(user.userId).subscribe({
        next: () => {
          this.refresh$.next();
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      });
    }
  }

  onAddRole(user: UserDto, role: { roleId: string; name: string }): void {
    this.usersService.addRoleToUser(user.userId, role.roleId).subscribe({
      next: () => {
        this.refresh$.next();
      },
      error: (error) => {
        console.error('Error adding role:', error);
      }
    });
  }

  onRemoveRole(user: UserDto, roleId: string): void {
    this.usersService.removeRoleFromUser(user.userId, roleId).subscribe({
      next: () => {
        this.refresh$.next();
      },
      error: (error) => {
        console.error('Error removing role:', error);
      }
    });
  }

  getAvailableRoles(user: UserDto, allRoles: { roleId: string; name: string }[]): { roleId: string; name: string }[] {
    const userRoleIds = new Set(user.roles.map(r => r.roleId));
    return allRoles.filter(r => !userRoleIds.has(r.roleId));
  }
}

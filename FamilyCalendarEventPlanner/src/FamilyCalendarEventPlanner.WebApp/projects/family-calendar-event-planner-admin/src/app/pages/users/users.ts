import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { BehaviorSubject, switchMap, forkJoin, of } from 'rxjs';
import { UsersService } from '../../services/users.service';
import { RolesService } from '../../services/roles.service';
import { UserDto, RoleDto } from '../../models/user-dto';
import { CreateOrEditUserDialog, CreateOrEditUserDialogResult } from '../../components/create-or-edit-user-dialog';
import { ConfirmDialog, ConfirmDialogResult } from '../../components/confirm-dialog';

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
    MatMenuModule,
    MatSnackBarModule
  ],
  templateUrl: './users.html',
  styleUrls: ['./users.scss']
})
export class Users {
  private usersService = inject(UsersService);
  private rolesService = inject(RolesService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

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
            this.snackBar.open('User created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error creating user:', error);
            this.snackBar.open('Error creating user', 'Close', { duration: 3000 });
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
            this.snackBar.open('User updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error updating user:', error);
            this.snackBar.open('Error updating user', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteUser(user: UserDto): void {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      width: '400px',
      data: {
        title: 'Delete User',
        message: `Are you sure you want to delete ${user.userName}?`
      }
    });

    dialogRef.afterClosed().subscribe((result: ConfirmDialogResult) => {
      if (result?.confirmed) {
        this.usersService.deleteUser(user.userId).subscribe({
          next: () => {
            this.refresh$.next();
            this.snackBar.open('User deleted successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            console.error('Error deleting user:', error);
            this.snackBar.open('Error deleting user', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onAddRole(user: UserDto, role: { roleId: string; name: string }): void {
    this.usersService.addRoleToUser(user.userId, role.roleId).subscribe({
      next: () => {
        this.refresh$.next();
        this.snackBar.open('Role added successfully', 'Close', { duration: 3000 });
      },
      error: (error) => {
        console.error('Error adding role:', error);
        this.snackBar.open('Error adding role', 'Close', { duration: 3000 });
      }
    });
  }

  onRemoveRole(user: UserDto, roleId: string): void {
    this.usersService.removeRoleFromUser(user.userId, roleId).subscribe({
      next: () => {
        this.refresh$.next();
        this.snackBar.open('Role removed successfully', 'Close', { duration: 3000 });
      },
      error: (error) => {
        console.error('Error removing role:', error);
        this.snackBar.open('Error removing role', 'Close', { duration: 3000 });
      }
    });
  }

  getAvailableRoles(user: UserDto, allRoles: { roleId: string; name: string }[]): { roleId: string; name: string }[] {
    const userRoleIds = new Set(user.roles.map(r => r.roleId));
    return allRoles.filter(r => !userRoleIds.has(r.roleId));
  }
}

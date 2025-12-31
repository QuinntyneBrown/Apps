import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { Observable } from 'rxjs';
import { AccountService } from '../services';
import { Account, CreateAccount, UpdateAccount, AccountCategory, AccountCategoryLabels, SecurityLevel, SecurityLevelLabels } from '../models';

@Component({
  selector: 'app-account-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.account ? 'Edit Account' : 'Add Account' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="account-form">
        <mat-form-field appearance="outline" class="account-form__field">
          <mat-label>Account Name</mat-label>
          <input matInput formControlName="accountName" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="account-form__field">
          <mat-label>Username</mat-label>
          <input matInput formControlName="username" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="account-form__field">
          <mat-label>Website URL</mat-label>
          <input matInput formControlName="websiteUrl" type="url">
        </mat-form-field>

        <mat-form-field appearance="outline" class="account-form__field">
          <mat-label>Category</mat-label>
          <mat-select formControlName="category" required>
            <mat-option *ngFor="let cat of categories" [value]="cat.value">
              {{ cat.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-checkbox formControlName="hasTwoFactorAuth" class="account-form__checkbox">
          Has Two-Factor Authentication
        </mat-checkbox>

        <mat-form-field appearance="outline" class="account-form__field">
          <mat-label>Last Password Change</mat-label>
          <input matInput formControlName="lastPasswordChange" type="datetime-local">
        </mat-form-field>

        <mat-form-field appearance="outline" class="account-form__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>

        <mat-checkbox *ngIf="data.account" formControlName="isActive" class="account-form__checkbox">
          Is Active
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .account-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
      padding: 1rem 0;
    }

    .account-form__field {
      width: 100%;
    }

    .account-form__checkbox {
      margin: 0.5rem 0;
    }

    @media (max-width: 768px) {
      .account-form {
        min-width: 300px;
      }
    }
  `]
})
export class AccountFormDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  data: { account?: Account } = {};

  form: FormGroup;
  categories = Object.keys(AccountCategory)
    .filter(key => !isNaN(Number(AccountCategory[key as any])))
    .map(key => ({
      value: AccountCategory[key as any],
      label: AccountCategoryLabels[AccountCategory[key as any]]
    }));

  constructor() {
    this.form = this.fb.group({
      accountName: ['', Validators.required],
      username: ['', Validators.required],
      websiteUrl: [''],
      category: [AccountCategory.Other, Validators.required],
      hasTwoFactorAuth: [false],
      lastPasswordChange: [''],
      notes: [''],
      isActive: [true]
    });

    if (this.data.account) {
      const account = this.data.account;
      this.form.patchValue({
        accountName: account.accountName,
        username: account.username,
        websiteUrl: account.websiteUrl,
        category: account.category,
        hasTwoFactorAuth: account.hasTwoFactorAuth,
        lastPasswordChange: account.lastPasswordChange ? new Date(account.lastPasswordChange).toISOString().slice(0, 16) : '',
        notes: account.notes,
        isActive: account.isActive
      });
    }
  }

  save() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        lastPasswordChange: formValue.lastPasswordChange || undefined
      };
      // dialogRef.close(result) would be called here, but we'll handle it in the parent
    }
  }
}

@Component({
  selector: 'app-accounts',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipModule,
    MatCardModule
  ],
  template: `
    <div class="accounts">
      <div class="accounts__header">
        <h1 class="accounts__title">Accounts</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="accounts__add-btn">
          <mat-icon>add</mat-icon>
          Add Account
        </button>
      </div>

      <mat-card class="accounts__card">
        <table mat-table [dataSource]="accounts$ | async" class="accounts__table">
          <ng-container matColumnDef="accountName">
            <th mat-header-cell *matHeaderCellDef>Account Name</th>
            <td mat-cell *matCellDef="let account">{{ account.accountName }}</td>
          </ng-container>

          <ng-container matColumnDef="username">
            <th mat-header-cell *matHeaderCellDef>Username</th>
            <td mat-cell *matCellDef="let account">{{ account.username }}</td>
          </ng-container>

          <ng-container matColumnDef="category">
            <th mat-header-cell *matHeaderCellDef>Category</th>
            <td mat-cell *matCellDef="let account">
              <mat-chip>{{ getCategoryLabel(account.category) }}</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="securityLevel">
            <th mat-header-cell *matHeaderCellDef>Security Level</th>
            <td mat-cell *matCellDef="let account">
              <mat-chip [class]="'accounts__chip--' + getSecurityLevelClass(account.securityLevel)">
                {{ getSecurityLevelLabel(account.securityLevel) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="hasTwoFactorAuth">
            <th mat-header-cell *matHeaderCellDef>2FA</th>
            <td mat-cell *matCellDef="let account">
              <mat-icon [color]="account.hasTwoFactorAuth ? 'primary' : ''">
                {{ account.hasTwoFactorAuth ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="isActive">
            <th mat-header-cell *matHeaderCellDef>Active</th>
            <td mat-cell *matCellDef="let account">
              <mat-icon [color]="account.isActive ? 'primary' : ''">
                {{ account.isActive ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let account">
              <button mat-icon-button color="primary" (click)="openDialog(account)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(account.accountId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .accounts {
      padding: 2rem;
    }

    .accounts__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .accounts__title {
      margin: 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .accounts__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .accounts__card {
      overflow-x: auto;
    }

    .accounts__table {
      width: 100%;
    }

    .accounts__chip--high {
      background-color: #4caf50 !important;
      color: white !important;
    }

    .accounts__chip--medium {
      background-color: #ff9800 !important;
      color: white !important;
    }

    .accounts__chip--low {
      background-color: #f44336 !important;
      color: white !important;
    }

    @media (max-width: 768px) {
      .accounts {
        padding: 1rem;
      }

      .accounts__header {
        flex-direction: column;
        align-items: flex-start;
        gap: 1rem;
      }
    }
  `]
})
export class Accounts implements OnInit {
  private accountService = inject(AccountService);
  private dialog = inject(MatDialog);

  accounts$: Observable<Account[]> = this.accountService.accounts$;
  displayedColumns = ['accountName', 'username', 'category', 'securityLevel', 'hasTwoFactorAuth', 'isActive', 'actions'];

  ngOnInit() {
    this.accountService.getAccounts().subscribe();
  }

  getCategoryLabel(category: AccountCategory): string {
    return AccountCategoryLabels[category];
  }

  getSecurityLevelLabel(level: SecurityLevel): string {
    return SecurityLevelLabels[level];
  }

  getSecurityLevelClass(level: SecurityLevel): string {
    switch (level) {
      case SecurityLevel.High: return 'high';
      case SecurityLevel.Medium: return 'medium';
      case SecurityLevel.Low: return 'low';
      default: return '';
    }
  }

  openDialog(account?: Account) {
    const dialogRef = this.dialog.open(AccountFormDialog, {
      width: '600px',
      data: { account }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (account) {
          const updateData: UpdateAccount = {
            accountId: account.accountId,
            ...result
          };
          this.accountService.updateAccount(updateData).subscribe();
        } else {
          const createData: CreateAccount = {
            userId: '00000000-0000-0000-0000-000000000000', // Default user ID
            ...result
          };
          this.accountService.createAccount(createData).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this account?')) {
      this.accountService.deleteAccount(id).subscribe();
    }
  }
}

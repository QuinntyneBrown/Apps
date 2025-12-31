import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { AccountService } from '../services';
import { Account, ACCOUNT_TYPE_LABELS } from '../models';

@Component({
  selector: 'app-accounts',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="accounts">
      <div class="accounts__header">
        <h1>Investment Accounts</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Account
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(accounts$ | async) || []" class="accounts__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let account">{{ account.name }}</td>
          </ng-container>

          <ng-container matColumnDef="type">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let account">{{ getTypeLabel(account.accountType) }}</td>
          </ng-container>

          <ng-container matColumnDef="institution">
            <th mat-header-cell *matHeaderCellDef>Institution</th>
            <td mat-cell *matCellDef="let account">{{ account.institution }}</td>
          </ng-container>

          <ng-container matColumnDef="balance">
            <th mat-header-cell *matHeaderCellDef>Balance</th>
            <td mat-cell *matCellDef="let account">{{ account.currentBalance | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let account">
              <mat-chip [color]="account.isActive ? 'primary' : 'warn'">
                {{ account.isActive ? 'Active' : 'Inactive' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let account">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteAccount(account)">
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
      padding: 1.5rem;
    }
    .accounts__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .accounts__table {
      width: 100%;
    }
  `]
})
export class Accounts implements OnInit {
  private _accountService = inject(AccountService);

  accounts$ = this._accountService.accounts$;
  displayedColumns = ['name', 'type', 'institution', 'balance', 'status', 'actions'];

  ngOnInit(): void {
    this._accountService.getAll().subscribe();
  }

  getTypeLabel(type: number): string {
    return ACCOUNT_TYPE_LABELS[type as keyof typeof ACCOUNT_TYPE_LABELS] || 'Unknown';
  }

  deleteAccount(account: Account): void {
    if (confirm(`Are you sure you want to delete "${account.name}"?`)) {
      this._accountService.delete(account.accountId).subscribe();
    }
  }
}

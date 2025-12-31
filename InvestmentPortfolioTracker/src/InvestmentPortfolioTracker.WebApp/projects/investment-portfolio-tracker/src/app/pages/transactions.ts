import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { TransactionService } from '../services';
import { Transaction, TRANSACTION_TYPE_LABELS } from '../models';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="transactions">
      <div class="transactions__header">
        <h1>Transactions</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Transaction
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(transactions$ | async) || []" class="transactions__table">
          <ng-container matColumnDef="date">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let transaction">{{ transaction.transactionDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="type">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let transaction">
              <mat-chip>{{ getTypeLabel(transaction.transactionType) }}</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="symbol">
            <th mat-header-cell *matHeaderCellDef>Symbol</th>
            <td mat-cell *matCellDef="let transaction">{{ transaction.symbol || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="shares">
            <th mat-header-cell *matHeaderCellDef>Shares</th>
            <td mat-cell *matCellDef="let transaction">{{ transaction.shares ? (transaction.shares | number:'1.2-4') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="price">
            <th mat-header-cell *matHeaderCellDef>Price</th>
            <td mat-cell *matCellDef="let transaction">{{ transaction.pricePerShare ? (transaction.pricePerShare | currency) : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="amount">
            <th mat-header-cell *matHeaderCellDef>Amount</th>
            <td mat-cell *matCellDef="let transaction">{{ transaction.amount | currency }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let transaction">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteTransaction(transaction)">
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
    .transactions {
      padding: 1.5rem;
    }
    .transactions__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .transactions__table {
      width: 100%;
    }
  `]
})
export class Transactions implements OnInit {
  private _transactionService = inject(TransactionService);

  transactions$ = this._transactionService.transactions$;
  displayedColumns = ['date', 'type', 'symbol', 'shares', 'price', 'amount', 'actions'];

  ngOnInit(): void {
    this._transactionService.getAll().subscribe();
  }

  getTypeLabel(type: number): string {
    return TRANSACTION_TYPE_LABELS[type as keyof typeof TRANSACTION_TYPE_LABELS] || 'Unknown';
  }

  deleteTransaction(transaction: Transaction): void {
    if (confirm('Are you sure you want to delete this transaction?')) {
      this._transactionService.delete(transaction.transactionId).subscribe();
    }
  }
}
